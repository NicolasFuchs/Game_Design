using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour {
    private GameObject player;
    private Controller controller;
    public Rigidbody2D rigidBody;

    public enum HarpoonModes {Launched,Hold}
    public HarpoonModes harpoonMode = HarpoonModes.Hold;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<Controller>();
        //rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if (harpoonMode == HarpoonModes.Launched)
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            rigidBody.velocity = new Vector3(0, -10.0f, 0);
        }

        if (transform.position.y < -15.0f)
            Object.Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collision = col.collider;
        if (collision.tag == "Fish")
        {
            controller.addFish();
            collision.GetComponent<Fish>().DestroyImself();
            Object.Destroy(this.gameObject);
        }
        if (collision.tag == "Orca")
        {
            collision.GetComponent<Orca>().BeHunter();
            Object.Destroy(this.gameObject);
        }
    }

    public void launch()
    {
        harpoonMode = HarpoonModes.Launched;
    }
}
