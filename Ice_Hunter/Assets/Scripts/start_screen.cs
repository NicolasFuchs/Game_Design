using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start_screen : MonoBehaviour {
    public Button btnQuit;
    public Button btnStart;

    void Start()
    {
        Button btnQ = btnQuit.GetComponent<Button>();
        btnQ.onClick.AddListener(OnQuit);
        Button btnS = btnStart.GetComponent<Button>();
        btnS.onClick.AddListener(OnStart);
    }

    void OnQuit()
    {
        Debug.Log("You have clicked the quit button!");
    }

    void OnStart()
    {
        Debug.Log("You have clicked the start button!");
    }
}
