using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Start_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        Collider2D collision = col.collider;
        if (collision.tag == "Fish")
        {
            collision.GetComponent<Fish>().ChangeDirection();
        }
        if (collision.tag == "Orca")
        {
            collision.GetComponent<Orca>().ChangeDirection();
        }
    }
}
