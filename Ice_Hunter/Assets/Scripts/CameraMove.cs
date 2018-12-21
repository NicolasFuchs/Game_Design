using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
        // Le perso bouge, le decor est fixe
        player = GameObject.FindWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
        float xSize = Screen.width;
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector3 playerScreenPos =  Camera.main.WorldToScreenPoint(player.transform.position);

        if(playerScreenPos.x <= xSize * 0.4f)
        {
            transform.position = new Vector3(player.transform.position.x + width*0.1f, transform.position.y, transform.position.z);

        }else if (playerScreenPos.x >= xSize * 0.6f)
        {
            transform.position = new Vector3(player.transform.position.x - width*0.1f, transform.position.y, transform.position.z);
        }
	}
}
