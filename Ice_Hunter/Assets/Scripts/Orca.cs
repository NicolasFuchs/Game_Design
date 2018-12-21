using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orca : MonoBehaviour {
    public enum Orca_Mode {Traveller, Hunter, Happy}
    public enum Direction { Left, Right }

    private float happyDuration = 30.0f;  // in secondes
    private float startTime;

    //private float xSpeed_Min = 2.0f;
    private float xSpeed = 8.1f;

    private float last_x_pos = 0;

    private float epsilon = 0.00000000000001f;

    private float ySpeed_Min = 2.0f; 
    private float ySpeed = 3;
    private float xPos=0.0f;
    private float yPos=0.0f;
    private float yMin = -12.0f;
    private float yMax_Hunt = -1.0f;
    private float yMax = -8.0f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private Sprite[] sprites;
    private float sprite_cpt;

    private enum Vertical_Direction { Up, Down }
    private enum Horizontal_Direction { Left, Right }

    private Vertical_Direction vertical_direction = Vertical_Direction.Down;
    private Horizontal_Direction horizontal_direction = Horizontal_Direction.Left;

    public Orca_Mode orca_mode = Orca_Mode.Traveller;

    // Use this for initialization
    void Start()
    {
        sprites = new Sprite[9];
        sprite_cpt = 0;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        xPos = rigidBody.position.x;
        last_x_pos = transform.position.x;
        yPos = rigidBody.position.y;

        horizontal_direction = Horizontal_Direction.Left;

        sprites[0] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[1] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[2] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[3] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[4] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[5] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[6] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[7] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;
        sprites[8] = Resources.Load("Orca/Orca_1", typeof(Sprite)) as Sprite;

        spriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        yPos = rigidBody.position.y;

        if (yPos < yMin)
        {
            vertical_direction = Vertical_Direction.Up;
        }

        if (orca_mode == Orca_Mode.Traveller &&  yPos > yMax)
        {
            vertical_direction = Vertical_Direction.Down;
        }
        if (yPos > yMax_Hunt)
        {
            vertical_direction = Vertical_Direction.Down;
        }

        GameObject pl = GameObject.FindWithTag("Player");
        Vector3 target = pl.transform.position;
        //Debug.Log("Distance x = " + Mathf.Abs(target.x - transform.position.x));

        if (orca_mode != Orca_Mode.Happy &&  Mathf.Abs(target.x - transform.position.x) < 20)
        {
            orca_mode = Orca_Mode.Hunter;
            if(target.x - transform.position.x >= 0.0f)
            {
                horizontal_direction = Horizontal_Direction.Right;
            }
            else
            {
                horizontal_direction = Horizontal_Direction.Left;
            }
        }
        else if (orca_mode != Orca_Mode.Happy)
        {
            orca_mode = Orca_Mode.Traveller;
        }

        if(orca_mode == Orca_Mode.Happy)
        {
            if (startTime + happyDuration < Time.time)
            {
                orca_mode = Orca_Mode.Traveller;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 velocity;
        spriteRenderer.sprite = sprites[(int)(sprite_cpt) % 9];
        sprite_cpt = (float)(sprite_cpt + (0.16));

        if(orca_mode == Orca_Mode.Traveller || orca_mode == Orca_Mode.Happy)
        {
            if (horizontal_direction == Horizontal_Direction.Left)
            {
                if (vertical_direction == Vertical_Direction.Up)
                {
                    velocity = new Vector3(-xSpeed, ySpeed, 0);
                }
                else
                {
                    velocity = new Vector3(-xSpeed, -ySpeed, 0);
                }

                spriteRenderer.flipX = true;
            }
            else
            {
                if (vertical_direction == Vertical_Direction.Up)
                {
                    velocity = new Vector3(xSpeed, ySpeed, 0);
                }
                else
                {
                    velocity = new Vector3(xSpeed, -ySpeed, 0);
                }

                spriteRenderer.flipX = false;
            }

            rigidBody.velocity = velocity;
        }
        else if (orca_mode == Orca_Mode.Hunter)
        {
            if (horizontal_direction == Horizontal_Direction.Left)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            GameObject pl = GameObject.FindWithTag("Player");
            Vector3 target = pl.transform.position;

            Vector3 direction = target - transform.position;
            rigidBody.velocity = direction;
        }

        if (Mathf.Abs(transform.position.x - last_x_pos) <= epsilon)
        {
            Vector3 v = transform.position;
            v.y = v.y - 0.05f;
            transform.position = v;
        }

        last_x_pos = transform.position.x;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        if (col.tag == "IceBlock")
        {
            switch (vertical_direction)
            {
                case Vertical_Direction.Down: vertical_direction = Vertical_Direction.Up; break;
                case Vertical_Direction.Up: vertical_direction = Vertical_Direction.Down; break;
                default: break;
            }
        }

    }

    public void ChangeDirection()
    {
        if (horizontal_direction == Horizontal_Direction.Left)
        {
            horizontal_direction = Horizontal_Direction.Right;
            //spriteRenderer.flipX = false;
        }
        else
        {
            horizontal_direction = Horizontal_Direction.Left;
            //spriteRenderer.flipX = true;
        }
    }

    public void BeHappy()
    {
        this.startTime = Time.time;
        orca_mode = Orca_Mode.Happy;
    }

    public void BeHunter()
    {
        orca_mode = Orca_Mode.Hunter;
    }
}

