using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public GameObject P_IceBlock_8;
    public GameObject P_IceBlock_4;
    public GameObject P_IceBlock_2;
    public GameObject P_IceBlock_1;

    public float xPos;
    public float yPos;
    private float yPosMin = -2.4f;

    private float yWater = 0;
    private int hitPoint = 10;
    private int strenghOrca = 10;
    private bool isPlayerOnBlock = false;
    //private int defaultDelay = 5;
    //private int delay;
    public enum IceBlockModes { Empty, Sinking, Rising };
    public IceBlockModes iceBlockMode = IceBlockModes.Empty;

    public float sinkingStep = 0.2f;
    public float risingStep = 0.4f;
    public int blockSize;
    public int startHitPoints = 10;

    GameObject sound;
    SoundManager sound_mng;

    // Use this for initialization
    void Start()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        hitPoint = startHitPoints;

        sound = GameObject.FindWithTag("Sound");
        sound_mng = sound.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = new Vector3(xPos, yPos, 0);
    }


    // TODO: Correct bug with sinking...
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        if (col.tag == "Player") 
        {
            isPlayerOnBlock = true;
            iceBlockMode = IceBlockModes.Sinking;
        }
        else if (col.tag == "Orca" && isPlayerOnBlock && col.GetComponent<Orca>().orca_mode == Orca.Orca_Mode.Hunter)
        {
            col.GetComponent<Orca>().BeHappy();

            hitPoint = hitPoint - strenghOrca;
            if (hitPoint <= 0) Break();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag == "Player")
        {
            isPlayerOnBlock = false;
            iceBlockMode = IceBlockModes.Empty;
        }
    }

    // Fixed delta time
    void FixedUpdate()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        Vector3 vector = new Vector3(0, 0, 0);

        if (isPlayerOnBlock)
        {
            vector = new Vector3(0, -sinkingStep, 0);
        }
        else if (!isPlayerOnBlock && (yWater > yPos)) //&& (delay <= 0))
        {
            vector = new Vector3(0, risingStep, 0);
        }

        // not more down than waterLevel
        transform.position += vector * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, yPosMin), 0);
    }

    void Break()
    {
        sound_mng.PlaySFX(SoundManager.SFX.ICEORCA);

        // Le bloc se brise en deux ou disparait si taille 1
        if (blockSize == 8)
        {
            Instantiate(P_IceBlock_4, new Vector3(transform.position.x - 8.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
            Instantiate(P_IceBlock_4, new Vector3(transform.position.x + 8.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
        }
        if (blockSize == 4)
        {
            Instantiate(P_IceBlock_2, new Vector3(transform.position.x - 4.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
            Instantiate(P_IceBlock_2, new Vector3(transform.position.x + 4.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
        }
        if (blockSize == 2)
        {
            Instantiate(P_IceBlock_1, new Vector3(transform.position.x - 2.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
            Instantiate(P_IceBlock_1, new Vector3(transform.position.x + 2.0f, transform.position.y - 0.1f, 0f), Quaternion.identity);
        }

        Object.Destroy(this.gameObject);
    }
}
