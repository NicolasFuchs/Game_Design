using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPoint : MonoBehaviour {

    public Text scoreText;
    private int vp = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVP(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
