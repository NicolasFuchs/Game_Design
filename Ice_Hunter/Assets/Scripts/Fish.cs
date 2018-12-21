using UnityEngine;

public class Fish : MonoBehaviour
{
    private float xSpeed_Max = 10.0f;
    private float xSpeed_Min = 5.0f;
    private float xSpeed = 8.1f;

    private float ySpeed_Max = 5.0f;
    private float ySpeed_Min = 1.0f;
    private float ySpeed = 3.0f;

    private float xPos = 0;
    private float yPos = 0;
    private float yMin = -8;
    private float yMax = -2;
    private int frame_counter = 0;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private Sprite[] sprites;
    private float sprite_cpt;

    private enum Vertical_Direction { Up, Down }
    private enum Horizontal_Direction { Left, Right }

    private Vertical_Direction vertical_direction = Vertical_Direction.Down;
    private Horizontal_Direction horizontal_direction = Horizontal_Direction.Left;

    // Use this for initialization
    void Start()
    {
        // 
        sprites = new Sprite[9];
        sprite_cpt = 0;
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        xPos = rigidBody.position.x;
        yPos = rigidBody.position.y;

        horizontal_direction = Horizontal_Direction.Left;

        sprites[0] = Resources.Load("Fish/Fish_1", typeof(Sprite)) as Sprite;
        sprites[1] = Resources.Load("Fish/Fish_2", typeof(Sprite)) as Sprite;
        sprites[2] = Resources.Load("Fish/Fish_3", typeof(Sprite)) as Sprite;
        sprites[3] = Resources.Load("Fish/Fish_4", typeof(Sprite)) as Sprite;
        sprites[4] = Resources.Load("Fish/Fish_5", typeof(Sprite)) as Sprite;
        sprites[5] = Resources.Load("Fish/Fish_4", typeof(Sprite)) as Sprite;
        sprites[6] = Resources.Load("Fish/Fish_3", typeof(Sprite)) as Sprite;
        sprites[7] = Resources.Load("Fish/Fish_2", typeof(Sprite)) as Sprite;
        sprites[8] = Resources.Load("Fish/Fish_1", typeof(Sprite)) as Sprite;

        spriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        frame_counter++;
        if (frame_counter >= 100) // each 2 second change the speed
        {
            frame_counter = 0;
            xSpeed = Random.Range(xSpeed_Min, xSpeed_Max);
            ySpeed = Random.Range(ySpeed_Min, ySpeed_Max);
        }

        yPos = rigidBody.position.y;

        if (yPos < yMin)
        {
            vertical_direction = Vertical_Direction.Up;
        }

        if (yPos > yMax)
        {
            vertical_direction = Vertical_Direction.Down;
        }
    }

    private void LateUpdate()
    {
        Vector3 velocity;
        spriteRenderer.sprite = sprites[(int)(sprite_cpt) % 9];
        sprite_cpt = (float)(sprite_cpt + (0.16));

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
        }

        rigidBody.velocity = velocity;
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
            spriteRenderer.flipX = true;
        }
        else
        {
            horizontal_direction = Horizontal_Direction.Left;
            spriteRenderer.flipX = false;
        }
    }

    public void DestroyImself()
    {
        Object.Destroy(this.gameObject);
    }
}
