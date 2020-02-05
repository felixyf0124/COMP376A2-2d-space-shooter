using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossA : MonoBehaviour {

    [SerializeField]
    float fireArea;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float firePeriod;
    [SerializeField]
    float fireBreak;

    [SerializeField]
    float cannon1WaveAng;
    [SerializeField]
    float cannon2WavePeriod;
    [SerializeField]
    float Speed;
    [SerializeField]
    Vector2 MoveArea;
    [SerializeField]
    float AmplitudeR;
    [SerializeField]
    float PeriodInFrame;
    [SerializeField]
    float offsetY;
    

    [SerializeField]
    Transform CannonL1;
    [SerializeField]
    Transform CannonR1;
    [SerializeField]
    Transform CannonL2;
    [SerializeField]
    Transform CannonR2;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    float angSpeed;

    [SerializeField]
    int CoreHealth;

    [SerializeField]
    GameObject ShieldGeneratorL;
    [SerializeField]
    GameObject ShieldGeneratorR;
    [SerializeField]
    GameObject shield;

    [SerializeField]
    int ShieldGeneratorHP;



    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject bossExplosion;

    public int scoreValue1;
    public int scoreValue2;
    public int scoreValue3;

    public float rebootTime;

    int ShieldGeneratorHPL;
    int ShieldGeneratorHPR;
    int NoOfGen;


    bool isMovingAround;

    bool isFiring;
    bool isEnergySheildOn;
    bool losingShield;
    bool rebootShield;

    Animator Animator;
    Rigidbody2D Rigidbody;

    //anima bool
    bool aniInitial;
    bool aniGeneratingShield;
    bool aniLosingShield;
    bool aniShieldOpening;
    bool aniShielClosing;

    private int NoOfShieldGenerator;
    private float nextReboot;

    private float direction1 ;
    private float direction2;
    private Quaternion rota;
    private float ang;
    private float startAng1;
    private float startAng2;

    private float NextFire;
    private float NextPeriod;

    private float X;
    private float Y;
    Vector2 pos;

    private Vector2 lGenOffset;
    private Vector2 rGenOffset;
    private Vector2 esOffset;

    Vector2 direction;
    float dist;


    public float animeTime;
    public float shieldingTime;
     float recoverDoneTime;
     float shieldGoneTime;

    private GameController gameController;


    GameObject SGenL;
    GameObject SGenR;
    GameObject ES;

    // Use this for initialization
    void Start () {

        GameObject gameControllerObj = GameObject.FindWithTag("GameController");

        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }
        

        ShieldGeneratorHPL = ShieldGeneratorHP;
        ShieldGeneratorHPR = ShieldGeneratorHP;
        NoOfGen = 2;



        isMovingAround = false;
        
        isFiring = false;
        isEnergySheildOn = true;
        losingShield = false;
        rebootShield = false;
        
        aniInitial = true;
        aniGeneratingShield = false;
        aniLosingShield = false;
        aniShieldOpening = false;
        aniShielClosing = false;

        NextPeriod = Time.time + firePeriod;
        //CannonL1.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonL1.rotation.z + startAng1));
        //CannonR1.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonR1.rotation.z - startAng1));
        startAng2 = Random.Range( -90.0f, 90.0f);
        CannonL2.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonL2.rotation.z + startAng2));
        CannonR2.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonR2.rotation.z - startAng2));

        aniInitial = true;

        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody = GetComponent<Rigidbody2D>();
        pos.x = Rigidbody.position.x;
        pos.y = Rigidbody.position.y;
        dist = X - 0.0f;
        //lastMoment = Time.time;
        angSpeed = 360.0f / PeriodInFrame;
        if (pos.x >= 0)
        {
            direction.x = 1;
        }
        if (pos.x < 0)
        {
            direction.x = -1;
        }
        if (pos.y >= 0)
        {
            direction.y = -1;
        }
        if (pos.y < 0)
        {
            direction.y = 1;
        }
        pos = transform.position;


        lGenOffset = new Vector2(0.81f,0.34f);

        esOffset = new Vector2(shield.transform.localScale.x, shield.transform.localScale.y);
          
    }
	
	// Update is called once per frame
	void Update () {
        //fire

        

        fire();

        Animator.SetBool("BossA", aniInitial);
        Animator.SetBool("EnergyShieldOn", aniGeneratingShield);
        Animator.SetBool("EnergyShieldOff", aniLosingShield);
        Animator.SetBool("ShieldOpen", aniShieldOpening);
        Animator.SetBool("ShieldClose", aniShielClosing);
        //if(aniGeneratingShield = false
        //aniLosingShield = false;
        //aniShieldOpening = false;
        //aniShielClosing = false;)

        //if (!isEnergySheildOn)
        //{
        //    GameObject EnergyShield = GameObject.Find("EnergyShield");
        //    if(EnergyShield != null)
        //    {
        //        Destroy(EnergyShield);
        //        nextReboot = Time.time + rebootTime;

        //    }
        //}

        if(Time.time>nextReboot && isEnergySheildOn == false && losingShield == false)
        {
            
            ShieldGeneratorHPL = 2;
            ShieldGeneratorHPR = 2;
            NoOfGen = 2;
            isEnergySheildOn = true;
            //aniGeneratingShield = true;
            rebootShield = true;
            recoverDoneTime = Time.time + animeTime;
        }
        if(Time.time > recoverDoneTime && rebootShield == true)
        {
            lgRecover();
            rgRecover();
            shieldRecover();
            rebootShield = false;
            isEnergySheildOn = true;
        }
        if (Time.time > shieldGoneTime && isEnergySheildOn == true && losingShield == true)
        {
            isEnergySheildOn = false;
            shieldHide();
            
            
                nextReboot = Time.time + rebootTime;
            losingShield = false;
            //rebootShield = true;
        }

        if(Rigidbody.position.y< offsetY+ MoveArea.y && isMovingAround == false)
        {
            isMovingAround = true;
        }
        movingAround();


    }

    public int CoreTakenDMG()
    {
        if (CoreHealth > 0)
        {
            CoreHealth--;
           if(DisplayHP()==0)
            {
                Instantiate(bossExplosion, gameObject.transform.position, gameObject.transform.rotation);
                gameController.AddScore(scoreValue3);

                Destroy(gameObject);
                return 2;
            }
            return 1;
        }
        return 0;
    }

    public int DisplayHP()
    {
        return CoreHealth;
    }

    private void lgHide()
    {
        ShieldGeneratorL.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.3f, 0.0f);

    }

    private void lgRecover()
    {
        ShieldGeneratorL.transform.position =
            new Vector3(transform.position.x + lGenOffset.x, transform.position.y - lGenOffset.y, -0.0001f);
    }

    private void rgHide()
    {
        ShieldGeneratorR.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.3f, 0.0f);

    }

    private void rgRecover()
    {
        ShieldGeneratorR.transform.position =
            new Vector3(transform.position.x - lGenOffset.x, transform.position.y - lGenOffset.y, -0.0001f);
    }

    private void shieldHide()
    {
        shield.transform.localScale = new Vector2(0.0f, 0.0f);
        shield.transform.position =
            new Vector3(transform.position.x, transform.position.y + 0.4f);

    }
    private void shieldRecover()
    {
        shield.transform.localScale = esOffset;
        shield.transform.position =
            new Vector2(transform.position.x, transform.position.y - 0.28f);

    }

    public int LSGTakenDMG()
    {
        if(ShieldGeneratorHPL>0)
        {
            ShieldGeneratorHPL--;
            if(ShieldGeneratorHPL == 0)
            {
                
                    Instantiate(explosion, ShieldGeneratorL.transform.position, ShieldGeneratorL.transform.rotation);
                   
                    NoOfGen--;
                lgHide();

                if (NoOfGen == 0)
                {

                    losingShield = true;
                    shieldGoneTime = Time.time + animeTime;
                    nextReboot = Time.time + rebootTime;
                    //shieldHide();
                    gameController.AddScore(scoreValue2);

                    return 2;
                }
                gameController.AddScore(scoreValue1);

                return 1;
            }
        }
        return 0;
    }

    

    public int RSGTakenDMG()
    {
        if (ShieldGeneratorHPR > 0)
        {
            ShieldGeneratorHPR--;
            if (ShieldGeneratorHPR == 0)
            {

                 
                    Instantiate(explosion, ShieldGeneratorR.transform.position, ShieldGeneratorR.transform.rotation);
                
                    NoOfGen--;
                rgHide();
                if (NoOfGen == 0)
                {
                   
                    losingShield = true;
                    shieldGoneTime = Time.time + animeTime;
                    nextReboot = Time.time + rebootTime;
                    gameController.AddScore(scoreValue2);

                    //shieldHide();
                    return 2;
                }

                gameController.AddScore(scoreValue1);
                return 1;

            }
        }
        return 0;
    }

    

    private void fire()
    {

        if (Time.time > NextFire && Time.time < NextPeriod)
        {
            NextFire = Time.time + fireRate;
            ang = cannon1WaveAng * Mathf.Sin(Time.time * cannon2WavePeriod / 360.0f);


            rota = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonL1.rotation.z + ang));
            Instantiate(Bullet, CannonL1.position, rota);

            rota = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonR1.rotation.z - ang));
            Instantiate(Bullet, CannonR1.position, rota);

            ang = angSpeed * (float)direction1 * Time.time;


            rota = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonL2.rotation.z - ang - startAng2));
            Instantiate(Bullet, CannonL2.position, rota);

            rota = Quaternion.Euler(new Vector3(0.0f, 0.0f, CannonR2.rotation.z + ang + startAng2));
            Instantiate(Bullet, CannonR2.position, rota);

        }
        else
        {

            if (Time.time >= (NextPeriod + fireBreak))
            {
                NextPeriod = Time.time + firePeriod + fireBreak;
            }
        }
        if (startMoving())
        {
            movingAround();
        }

    }


    private void movingAround()
    {


        Y = direction.y * Speed  * Time.deltaTime;
        if (isMovingAround)
        {
            X = direction.x * Speed * Time.deltaTime;
            
        }
        else
        {
            X = 0.0f;
            
        }
        checkDirect();
        transform.position = new Vector2(Rigidbody.position.x + X, Rigidbody.position.y + Y);

    }

    private void checkDirect()
    {
        if (Rigidbody.position.y >= (MoveArea.y + offsetY) && direction.y >= 0)
        {

            direction.y = -1;

        }
        if (Rigidbody.position.y < (-MoveArea.y + offsetY) && direction.y < 0)
        {
            direction.y = 1;
        }
        if (Rigidbody.position.x >= MoveArea.x && direction.x >= 0)
        {

            direction.x = -1;

        }
        if (Rigidbody.position.x < -MoveArea.x && direction.x < 0)
        {
            direction.x = 1;
        }
    }


    private bool atkAvailable()
    {

        if (Rigidbody.position.y < fireArea)
        {
            return true;
        }
        else
            return false;
    }

    private bool startMoving()
    {
        if (Rigidbody.position.y < MoveArea.y)
        {
            return true;
        }
        else
            return false;
    }
    //private bool allowMove()
    //{
    //    if (Rigidbody.position.y < MoveValue.y+1 
    //        )
    //    {
    //        return true;
    //    }
    //    else
    //        return false;
    //}
    
}
