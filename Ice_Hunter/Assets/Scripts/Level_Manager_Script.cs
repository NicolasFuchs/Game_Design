using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager_Script : MonoBehaviour {
    public GameObject P_IceBlock_8;
    public GameObject P_IceBlock_4;
    public GameObject P_IceBlock_2;
    public GameObject P_IceBlock_1;

    public GameObject P_Fish;
    public GameObject P_Orcas;

    public List<GameObject> Ice_Blocks;
    public List<GameObject> Fishes;
    public List<GameObject> Orcas;

    // Use this for initialization
    void Start () {
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_8, new Vector3(-10f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_2, new Vector3(10f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_4, new Vector3(30f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_2, new Vector3(50f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_1, new Vector3(60f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_4, new Vector3(70f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_2, new Vector3(90f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_8, new Vector3(110f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_4, new Vector3(130, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_4, new Vector3(150f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_1, new Vector3(160f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_2, new Vector3(170f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_1, new Vector3(180f, 0f, 0f), Quaternion.identity));
        Ice_Blocks.Add((GameObject)Instantiate(P_IceBlock_2, new Vector3(190f, 0f, 0f), Quaternion.identity));

        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(30f, -4f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(60f, -5f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(75f, -4f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(100f, -7f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(115f, -8f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(135f, -4f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(160f, -6f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(45f, -7f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(170f, -5f, 0f), Quaternion.identity));
        Fishes.Add((GameObject)Instantiate(P_Fish, new Vector3(90f, -6f, 0f), Quaternion.identity));

        Orcas.Add((GameObject)Instantiate(P_Orcas, new Vector3(0f, -5f, 0f), Quaternion.identity));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
