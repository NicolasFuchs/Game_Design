using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public GameObject P_Harpoon;
    private GameObject harpoon = null;
    private BoxCollider2D iceBlockCollider = null;

    private Rigidbody2D rigidBody;
    public float speed = 30f;
    public float speedWater = 10f;
    public float speedClimbing = 5.0f;
    public float jumpForce = 100f;
    public GameObject footPosition;
    public LayerMask jumpMask;

    private Sprite[] sprites;
    private float sprite_cpt;
    private Sprite holding_sprite;
    private Sprite jumping_sprite;
    private Sprite[] sprite_swimming;
    private float sprite_swimming_cpt;
    private Sprite crouching;
    private Sprite scroutchingHarpoon;
    private Sprite[] sprites_climbing;
    private float sprite_climbing_cpt;

    private SpriteRenderer spriteRenderer;
    public float xPos = 0;
    public float yPos = 0;
    private Collider2D iceBlock;
    private BoxCollider2D playerCollider;

    //private enum Horizontal_Direction { Left, Right }
    //private Horizontal_Direction horizontal_direction = Horizontal_Direction.Left;

    private float horizontalSpeed = 0.0f;
    private float horizontalSpeed_abs = 0.0f;
    bool arrowDown = false;
    private bool directionLeft = false;
    private bool directionRight = true;
    private bool directionNot = false;
    private bool isNear = false;
    private float holdingSpeed = 0.001f;

    public enum PlayerModes { Walking, Jumping, Swimming, Fighting, Crouching, Drowning, Climbing, Holding, FishingCrouching}
    public enum PlayerWaterModes { InWater, OffWater}
    public PlayerModes playerMode = PlayerModes.Walking;
    public PlayerWaterModes playerWaterMode = PlayerWaterModes.OffWater;
    public enum GameMode {harvest,emergency,race}
    public GameMode gameMode = GameMode.harvest;

    private Heart heart_script;
    private float timeIn;

    // Audio Resources Load
    GameObject sound;
    SoundManager sound_mng;

    int VP = 0;
    float TimeCounter;

    float levelEmergency = 120;
    float levelRace = 10;
    float levelDie = 10;

    bool isDead;
    float DeadCounter;
    bool finishGamePossible;

    GameObject snow ;

    // Use this for initialization
    void Start()
    {
        snow = GameObject.FindWithTag("Snow");

        finishGamePossible = false;
        isDead = false;
        TimeCounter = Time.time;
        // Audio Resources Load
        sound = GameObject.FindWithTag("Sound");
        sound_mng = sound.GetComponent<SoundManager>();
        sound_mng.SetWindLevel(SoundManager.WindLevel.BREEZE);

        // Called once, when starting the scene
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
  
        sprites = new Sprite[18];
        sprite_cpt = 0;
        sprite_swimming = new Sprite[12];
        sprite_swimming_cpt = 0;
        sprites_climbing = new Sprite[6];
        sprite_climbing_cpt = 0;


        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        GameObject go = GameObject.FindWithTag("PlayerBody");
        spriteRenderer = go.GetComponent<SpriteRenderer>();

        GameObject go_heart = GameObject.FindWithTag("Heart");
        heart_script = go_heart.GetComponent<Heart>();
        timeIn = 0;

        xPos = rigidBody.position.x;
        yPos = rigidBody.position.y;

        //horizontal_direction = Horizontal_Direction.Left;

        sprites[0] = Resources.Load("Player/Running/Running_Girl01", typeof(Sprite)) as Sprite;
        sprites[1] = Resources.Load("Player/Running/Running_Girl02", typeof(Sprite)) as Sprite;
        sprites[2] = Resources.Load("Player/Running/Running_Girl03", typeof(Sprite)) as Sprite;
        sprites[3] = Resources.Load("Player/Running/Running_Girl04", typeof(Sprite)) as Sprite;
        sprites[4] = Resources.Load("Player/Running/Running_Girl05", typeof(Sprite)) as Sprite;
        sprites[5] = Resources.Load("Player/Running/Running_Girl06", typeof(Sprite)) as Sprite;
        sprites[6] = Resources.Load("Player/Running/Running_Girl07", typeof(Sprite)) as Sprite;
        sprites[7] = Resources.Load("Player/Running/Running_Girl08", typeof(Sprite)) as Sprite;
        sprites[8] = Resources.Load("Player/Running/Running_Girl09", typeof(Sprite)) as Sprite;
        sprites[9] = Resources.Load("Player/Running/Running_Girl10", typeof(Sprite)) as Sprite;
        sprites[10] = Resources.Load("Player/Running/Running_Girl11", typeof(Sprite)) as Sprite;
        sprites[11] = Resources.Load("Player/Running/Running_Girl12", typeof(Sprite)) as Sprite;
        sprites[12] = Resources.Load("Player/Running/Running_Girl13", typeof(Sprite)) as Sprite;
        sprites[13] = Resources.Load("Player/Running/Running_Girl14", typeof(Sprite)) as Sprite;
        sprites[14] = Resources.Load("Player/Running/Running_Girl15", typeof(Sprite)) as Sprite;
        sprites[15] = Resources.Load("Player/Running/Running_Girl16", typeof(Sprite)) as Sprite;
        sprites[16] = Resources.Load("Player/Running/Running_Girl17", typeof(Sprite)) as Sprite;
        sprites[17] = Resources.Load("Player/Running/Running_Girl18", typeof(Sprite)) as Sprite;

        holding_sprite = Resources.Load("Player/Standing_Girl", typeof(Sprite)) as Sprite;
        jumping_sprite = Resources.Load("Player/Jumping_Girl", typeof(Sprite)) as Sprite;
        crouching = Resources.Load("Player/Crouching_Girl", typeof(Sprite)) as Sprite;
        scroutchingHarpoon = Resources.Load("Player/Phishing_Girl", typeof(Sprite)) as Sprite;

        sprite_swimming[0] = Resources.Load("Player/Swimming/Swimming_Girl01", typeof(Sprite)) as Sprite;
        sprite_swimming[1] = Resources.Load("Player/Swimming/Swimming_Girl02", typeof(Sprite)) as Sprite;
        sprite_swimming[2] = Resources.Load("Player/Swimming/Swimming_Girl03", typeof(Sprite)) as Sprite;
        sprite_swimming[3] = Resources.Load("Player/Swimming/Swimming_Girl04", typeof(Sprite)) as Sprite;
        sprite_swimming[4] = Resources.Load("Player/Swimming/Swimming_Girl05", typeof(Sprite)) as Sprite;
        sprite_swimming[5] = Resources.Load("Player/Swimming/Swimming_Girl06", typeof(Sprite)) as Sprite;
        sprite_swimming[6] = Resources.Load("Player/Swimming/Swimming_Girl07", typeof(Sprite)) as Sprite;
        sprite_swimming[7] = Resources.Load("Player/Swimming/Swimming_Girl08", typeof(Sprite)) as Sprite;
        sprite_swimming[8] = Resources.Load("Player/Swimming/Swimming_Girl09", typeof(Sprite)) as Sprite;
        sprite_swimming[9] = Resources.Load("Player/Swimming/Swimming_Girl10", typeof(Sprite)) as Sprite;
        sprite_swimming[10] = Resources.Load("Player/Swimming/Swimming_Girl11", typeof(Sprite)) as Sprite;
        sprite_swimming[11] = Resources.Load("Player/Swimming/Swimming_Girl12", typeof(Sprite)) as Sprite;

        sprites_climbing[0] = Resources.Load("Player/Rising/Rising_Girl_01", typeof(Sprite)) as Sprite;
        sprites_climbing[1] = Resources.Load("Player/Rising/Rising_Girl_02", typeof(Sprite)) as Sprite;
        sprites_climbing[2] = Resources.Load("Player/Rising/Rising_Girl_03", typeof(Sprite)) as Sprite;
        sprites_climbing[3] = Resources.Load("Player/Rising/Rising_Girl_04", typeof(Sprite)) as Sprite;
        sprites_climbing[4] = Resources.Load("Player/Rising/Rising_Girl_05", typeof(Sprite)) as Sprite;
        sprites_climbing[5] = Resources.Load("Player/Rising/Rising_Girl_06", typeof(Sprite)) as Sprite;
    }

    private void updateStateVariables()
    {
        horizontalSpeed = Input.GetAxis("Horizontal");
        horizontalSpeed_abs = Mathf.Abs(horizontalSpeed);
        arrowDown = Input.GetAxis("Vertical") < 0.0f;

        if (horizontalSpeed < 0)
        {
            directionLeft = true;
            directionRight = false;
        }
        else if (horizontalSpeed > 0)
        {
            directionLeft = false;
            directionRight = true;
        }

        if(null == iceBlockCollider)
        {
            isNear = false;
        }
        else
        {
            float xIceSide = iceBlockCollider.bounds.center.x;
            if (directionLeft)
            {
                xIceSide = xIceSide - iceBlockCollider.bounds.extents.x;
            }
            else
            {
                xIceSide = xIceSide + iceBlockCollider.bounds.extents.x;
            }

            float w = this.GetComponent<BoxCollider2D>().bounds.extents.x - 0.1f;
            if (Mathf.Abs(transform.position.x - xIceSide) < w)
            {
                isNear = true;
            }
            else
            {
                isNear = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && Time.time - DeadCounter > 5.0f)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
        }

        if (isDead)
        {
            return;
        }

        if (Time.time - TimeCounter > levelEmergency)
        {
            gameMode = GameMode.emergency;
        }
        if(Time.time - TimeCounter > levelEmergency + levelRace)
        {
            gameMode = GameMode.race;
        }
        if (Time.time - TimeCounter > levelEmergency + levelRace + levelDie)
        {
            loseGame();
        }

        updateStateVariables();

        if (transform.position.y < 1.8)
        {
            if(playerWaterMode == PlayerWaterModes.OffWater)
            {
                timeIn = Time.time;
            }
            playerWaterMode = PlayerWaterModes.InWater;

            if(Time.time - timeIn > 1.0f)
            {
                timeIn = Time.time;

                int deg = heart_script.getHeartPos();
                if (deg <= 25)
                {
                    loseGame();
                }
                else
                {
                    heart_script.setHeartPos(deg - 1);
                }
            }
        }
        else
        {
            if (playerWaterMode == PlayerWaterModes.InWater)
            {
                timeIn = Time.time;
            }
            playerWaterMode = PlayerWaterModes.OffWater;

            if (Time.time - timeIn > 5.0f)
            {
                timeIn = Time.time;

                int deg = heart_script.getHeartPos();
                heart_script.setHeartPos(deg + 1); 
            }
        }

        if (playerMode == PlayerModes.Holding)
        {
            if(horizontalSpeed_abs > 0.001)
                playerMode = PlayerModes.Walking;
            else
                if (arrowDown)
                    if(isNear)
                        playerMode = PlayerModes.FishingCrouching;
                    else
                        playerMode = PlayerModes.Crouching;
        }
        else if (playerMode == PlayerModes.Walking)
        {
            if(horizontalSpeed_abs < holdingSpeed)
            {
                playerMode = PlayerModes.Holding;
            }
        }else if(playerMode == PlayerModes.Climbing)
        {
            if (null == iceBlock)
            {
                playerMode = PlayerModes.Walking;
            }
            if (playerCollider.bounds.center.y - playerCollider.bounds.extents.y > iceBlock.bounds.center.y + iceBlock.bounds.extents.y)
            {
                rigidBody.velocity = new Vector3(horizontalSpeed, 0, 0);
                playerMode = PlayerModes.Walking;
            }
            else
            {
                rigidBody.velocity = new Vector3(0, speedClimbing, 0);
            }
        }else if (playerMode == PlayerModes.FishingCrouching)
        {
            if (!arrowDown)
            {
                playerMode = PlayerModes.Walking;
            }
        }else if (playerMode == PlayerModes.Crouching)
        {
            if (!arrowDown)
            {
                playerMode = PlayerModes.Walking;
            }
        }
    }

    void LateUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (playerMode == PlayerModes.Walking)
        {
            destroyHarpoon();
            spriteRenderer.sprite = sprites[(int)(sprite_cpt) % 18];
            sprite_cpt = (float)(sprite_cpt + (0.32));
            walk();
            testJump();
        }
        else if (playerMode == PlayerModes.Holding)
        {
            destroyHarpoon();
            spriteRenderer.sprite = holding_sprite;
            testJump();
        }
        else if (playerMode == PlayerModes.Jumping)
        {
            destroyHarpoon();
            spriteRenderer.sprite = jumping_sprite;
            walk();
        }
        else if (playerMode == PlayerModes.Swimming)
        {
            destroyHarpoon();
            spriteRenderer.sprite = sprite_swimming[(int)(sprite_swimming_cpt) % 12];
            sprite_swimming_cpt = (float)(sprite_swimming_cpt + (0.18));
            // sprite
            swimm();
        }
        else if (playerMode == PlayerModes.FishingCrouching)
        {
            spriteRenderer.sprite = scroutchingHarpoon;
            createHarpoon();
            testLaunchHarpoon();
        }
        else if (playerMode == PlayerModes.Crouching)
        {
            spriteRenderer.sprite = crouching;
        }
        else if (playerMode == PlayerModes.Climbing)
        {
            spriteRenderer.sprite = sprites_climbing[(int)(sprite_climbing_cpt) % 6];
            sprite_climbing_cpt = (float)(sprite_climbing_cpt + (0.6));
        }

        if(gameMode == GameMode.emergency)
        {
            snow.GetComponent<ParticleSystem>().emissionRate = 3000.0f;
            sound_mng.SetWindLevel(SoundManager.WindLevel.LIGHT);
        }
        if (gameMode == GameMode.race)
        {
            sound_mng.SetWindLevel(SoundManager.WindLevel.MEDIUM);
            snow.GetComponent<ParticleSystem>().emissionRate = 6000.0f;
        }
    }

    public void destroyHarpoon()
    {
        if(null != harpoon)
        {
            GameObject.Destroy(harpoon);
        }
    }

    public void createHarpoon()
    {
        if (null == harpoon)
        {
            float deltaX = playerCollider.bounds.extents.x - 0.1f;
            if(directionLeft)
            {
                deltaX = -deltaX;
            }
            harpoon = Instantiate(P_Harpoon, new Vector3(transform.position.x + deltaX, transform.position.y, 0f), Quaternion.identity);
        }
    }

    private bool testLaunchHarpoon()
    {
        if (null != harpoon && harpoon.GetComponent<Harpoon>().harpoonMode != Harpoon.HarpoonModes.Launched && Input.GetButtonDown("LaunchHarpoon"))
        {
            harpoon.GetComponent<Harpoon>().launch();
            return true;
        }

        return false;
    }

    private void swimm()
    {
        // movement
        rigidBody.velocity = new Vector3(horizontalSpeed * speedWater, 0, 0);

        // orientation
        Vector3 scale = transform.localScale;
        if (rigidBody.velocity.x > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (rigidBody.velocity.x < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    private bool testJump()
    {
        float waterFactor = 1.0f;
        //float verticalSpeed = rigidBody.velocity.y;

        if (playerWaterMode == PlayerWaterModes.InWater)
        {
            waterFactor = 0.7f;
        }
        bool canJump = Physics2D.OverlapCircle(footPosition.transform.position, 0.25f, jumpMask);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.AddForce(new Vector3(0, jumpForce * waterFactor, 0), ForceMode2D.Impulse);
            playerMode = PlayerModes.Jumping;
            return true;
        }

        return false;
    }

    private void walk()
    {
        // movement
        float waterFactor = 1.0f;
        float verticalSpeed = rigidBody.velocity.y;

        if (playerWaterMode == PlayerWaterModes.InWater)
        {
            waterFactor = 0.7f;
        }
        rigidBody.velocity = new Vector3(horizontalSpeed * speed * waterFactor, verticalSpeed, 0);

        // orientation
        Vector3 scale = transform.localScale;

        if (directionRight)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (directionLeft)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        if (col.tag == "Orca")
        {
            col.GetComponent<Orca>().BeHappy();

            int deg = heart_script.getHeartPos();
            deg = deg - 2;
            heart_script.setHeartPos(deg);
            if (deg <= 25)
            {
                loseGame();
            }
        }
        if (col.tag == "WaterLevel")
        {
            playerMode = PlayerModes.Swimming;
            finishGamePossible = true;
        }
        if (col.tag == "IceBlock")
        {
            if (playerMode == PlayerModes.Swimming)
            {
                playerMode = PlayerModes.Climbing;
                iceBlock = col;
            }else if (playerMode == PlayerModes.Jumping)
            {
                playerMode = PlayerModes.Walking;
            }

            iceBlockCollider = col.GetComponent<BoxCollider2D>();
            finishGamePossible = true;
        }
        if (col.tag == "Landmass_Left")
        {
            if (playerMode == PlayerModes.Swimming)
            {
                playerMode = PlayerModes.Climbing;
                iceBlock = col;
            }
            else if (playerMode == PlayerModes.Jumping)
            {
                playerMode = PlayerModes.Walking;
            }

            iceBlockCollider = col.GetComponent<BoxCollider2D>();
        }
        if (col.tag == "Landmass_Right")
        {
            if (playerMode == PlayerModes.Swimming)
            {
                playerMode = PlayerModes.Climbing;
                iceBlock = col;
            }
            else if (playerMode == PlayerModes.Jumping)
            {
                playerMode = PlayerModes.Walking;
            }

            iceBlockCollider = col.GetComponent<BoxCollider2D>();

            if(gameMode == GameMode.harvest)
            {
                gameMode = GameMode.emergency;
                VP = VP + 100;
                GameObject go = GameObject.FindWithTag("VictoryPoint");
                go.GetComponent<VictoryPoint>().SetVP(VP);
            }
        }
        if (col.tag == "Igloo")
        {
            if (gameMode == GameMode.emergency || gameMode == GameMode.race || finishGamePossible)
            {
                SceneManager.LoadScene("end_screen");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D col = collision.collider;

        if (col.tag == "IceBlock")
        {
            iceBlockCollider = null;
        }
        if (col.tag == "Landmass_Left")
        {
            iceBlockCollider = null;
        }
        if (col.tag == "Landmass_Right")
        {
            iceBlockCollider = null;
        }
    }

    public void startFishing()
    {
        playerMode = PlayerModes.FishingCrouching;

        if(null != harpoon)
        {
            GameObject.Destroy(harpoon);
        }

        harpoon = (GameObject)Instantiate(P_Harpoon, new Vector3(transform.position.x + 1.0f, transform.position.y - 0.0f, 0f), Quaternion.identity);
    }

    public void addFish()
    {
        VP = VP + 10;

        GameObject go = GameObject.FindWithTag("VictoryPoint");
        go.GetComponent<VictoryPoint>().SetVP(VP);
    }

    public void loseGame()
    {
        isDead = true;
        DeadCounter = Time.time;
    }
}
