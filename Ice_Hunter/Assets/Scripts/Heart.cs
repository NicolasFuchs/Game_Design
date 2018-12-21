using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private float sprite_cpt;

    private float[] heartPos;

    private int heartDegre;

    // Use this for initialization
    void Start () {
        //GameObject cam = GameObject.FindWithTag("MainCamera");
        GameObject go = GameObject.FindWithTag("Heart");

        heartDegre = 37;

        spriteRenderer = go.GetComponent<SpriteRenderer>();
        sprite_cpt = 0;

        heartPos = new float[13];
        heartPos[0] = -9.0f;
        heartPos[1] = -7.5f;
        heartPos[2] = -6f;
        heartPos[3] = -4.5f;
        heartPos[4] = -3f;
        heartPos[5] = -1.5f;
        heartPos[6] = 0f;
        heartPos[7] = 1.5f;
        heartPos[8] = 3f;
        heartPos[9] = 4.5f;
        heartPos[10] = 6f;
        heartPos[11] = 7.5f;
        heartPos[12] = 9.0f;

        sprites = new Sprite[10];

        sprites[0] = Resources.Load("Score/Hearts/Heart0", typeof(Sprite)) as Sprite;
        sprites[1] = Resources.Load("Score/Hearts/Heart1", typeof(Sprite)) as Sprite;
        sprites[2] = Resources.Load("Score/Hearts/Heart2", typeof(Sprite)) as Sprite;
        sprites[3] = Resources.Load("Score/Hearts/Heart3", typeof(Sprite)) as Sprite;
        sprites[4] = Resources.Load("Score/Hearts/Heart4", typeof(Sprite)) as Sprite;
        sprites[5] = Resources.Load("Score/Hearts/Heart5", typeof(Sprite)) as Sprite;
        sprites[6] = Resources.Load("Score/Hearts/Heart6", typeof(Sprite)) as Sprite;
        sprites[7] = Resources.Load("Score/Hearts/Heart7", typeof(Sprite)) as Sprite;
        sprites[8] = Resources.Load("Score/Hearts/Heart8", typeof(Sprite)) as Sprite;
        sprites[9] = Resources.Load("Score/Hearts/Heart9", typeof(Sprite)) as Sprite;

    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sprite = sprites[(int)(sprite_cpt) % 10];
        sprite_cpt = (float)(sprite_cpt + (0.16));
    }

    public void setHeartPos(int degre)
    {
        GameObject score = GameObject.FindWithTag("Score");
        float x = score.transform.position.x;

        degre = Mathf.Min(Mathf.Max(degre, 25), 37);
        heartDegre = degre;
        degre = degre - 25;

        x = x + heartPos[degre];

        Vector3 vect = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = vect;
    }

    public int getHeartPos()
    {
        return heartDegre;
    }
}
