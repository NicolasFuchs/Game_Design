using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Start_Script : MonoBehaviour {
    public Button btnStart;
    public Button btnQuit;
    private float delay;

    void startGame()
    {
        Debug.Log("startGame Start_Script");
        SceneManager.LoadScene("level_01");
        delay = Time.time;
    }

    void quitGame()
    {
        Debug.Log("quitGame Start_Script");
        Application.Quit();
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start Start_Script");
        btnStart.onClick.AddListener(startGame);
        btnQuit.onClick.AddListener(quitGame);
        Debug.Log("Start Start_Script end");

    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time - delay > 5.0)
        {
            SceneManager.LoadScene("level_01");
        }
	}


}
