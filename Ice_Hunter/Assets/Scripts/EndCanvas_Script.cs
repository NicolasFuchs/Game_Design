using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvas_Script : MonoBehaviour {
    float delay;

	// Use this for initialization
	void Start () {
        delay = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - delay > 5.0f)
        {
            SceneManager.LoadScene("level_01");
        }
	}
}
