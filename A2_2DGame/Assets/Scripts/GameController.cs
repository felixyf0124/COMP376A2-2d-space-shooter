using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyAWaveSetting
{
    public int NoOfShips;
    
    public GameObject TypeOfShip;

    public float hDistance;

    public float NextShipSec;
    
    public Vector2 spawnValues;
}

[System.Serializable]
public class EnemyBWaveSetting
{
    public int NoOfShips;

    public GameObject TypeOfShip;

    public float NextShipSec;

    public Vector2 spawnValues;

    public float FireHoldArea;
}


public class GameController : MonoBehaviour {


   // private Player player;

    public GameObject powerUp;

    public bool isBulletHell = false;

    public EnemyAWaveSetting EnemyGroupA;
    //GameObject enemyA;

    [SerializeField]
    EnemyBWaveSetting EnemyGroupB;

    public BossA bossA;

    public float spawnPosBoss;

   

    public float bossComing;

    public float waitingT;

    [SerializeField]
    float NextSpawnWaveSec;

    [SerializeField]
    int waves;

    [SerializeField]
    bool Looping;

    Vector2 spawnPosition;
    Quaternion spawnRotation;

    bool isA;
    float wCounter;

    public float bgScrollingSpeed;
    private float currentScrollSpeed;
    private float srollslowdown;
    private float scrOffset;

    public Text ScoreText;

    public Text restartText;

    public Text gameOverText;

    public Text playerLv;

    public Text bossHP;

    public Text Main;

    public Text isHell;

    private bool gameOver;
    private bool restart;
    private int Score;
    private bool bkToMainAva;
    private float textDelay;

    public int[] lastWaveA;
    public int[] lastWaveB;
    public int[] currentWaveA;
    public int[] currentWaveB;
    public int lastWaveKillA;
    public int lastWaveKillB;
    public int currentWaveKillA;
    public int currentWaveKillB;

    public int someid;

    bool isBossFight;
    bool shown;

    // Use this for initialization
    void Start() {

        Score = 0;
        gameOver = false;
        restart = false;
        isBossFight = false;
        bkToMainAva = false;
        shown = false;
        restartText.text = "";
        gameOverText.text = "";
        Looping = true;
        spawnRotation = Quaternion.identity;
        isA = !false;
        lastWaveKillA = 0;
        lastWaveKillB = 0;
        currentWaveKillA = 0;
        currentWaveKillB = 0;
        scrOffset = 0.0f;
        //player = GameObject.FindWithTag("Player");
        //GameObject playerObj = GameObject.FindWithTag("Player");

        //if (playerObj != null)
        //{
        //    player = playerObj.GetComponent<Player>();
        //}
        //if (player == null)
        //{
        //    Debug.Log("Cannot find 'GameController' script.");
        //}

        UpdateScore();
        //UpdateLevel();
        
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (gameOver)
        {
            restartText.text = "Press 'R' to Restart";
            //Main.text = "Press 'M' return to Main Menu";
            restart = true;
            //bkToMainAva = true;
        }

        if(Looping && !isBossFight)
        {
           
            Looping = false;
            StartCoroutine(SpawnWaves());
            
        }
        
        if (isBossFight)
        {
            if (currentScrollSpeed>0)
            {
                currentScrollSpeed -= 0.01f;
                if(currentScrollSpeed <0)
                {
                    currentScrollSpeed = 0;
                }
            }
        }

        if (shown && Time.time > textDelay)
        {
            isHell.text = "";
            shown = false;
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            isBulletHell = !isBulletHell;
            if(isBulletHell)
            {
                isHell.text = "Hell Mode Is On";
                Invoke(isHell.text, 1.0f);
                textDelay = Time.time + waitingT;
                shown = true;
            }else
            {
                isHell.text = "Hell Mode Is Off";
                Invoke(isHell.text, 1.0f);
                textDelay = Time.time + waitingT;
                shown = true;

            }
        }

        if (bkToMainAva)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Application.LoadLevel("0_MainMenu");
            }
        }

    }

    IEnumerator SpawnWaves()
    {
        lastWaveA = new int[EnemyGroupA.NoOfShips];
        lastWaveB = new int[EnemyGroupB.NoOfShips];
        currentWaveA = new int[EnemyGroupA.NoOfShips];
        currentWaveB = new int[EnemyGroupB.NoOfShips];

        currentScrollSpeed = bgScrollingSpeed;

        while (!isBossFight)
        {
            wCounter = 0;
            while (wCounter < waves)
            {
                if (gameOver)
                {
                    break;
                }

                if (isA)
                {
                    int counter = 0;
                    int leader = v_leaderGenerator();
                    float positionX = Random.Range(-EnemyGroupA.spawnValues.x, EnemyGroupA.spawnValues.x);

                    float positionY = EnemyGroupA.spawnValues.y;
                    float lx, rx;
                    int left, right;
                    left = leader - 1;
                    right = leader + 1;

                    spawnPosition = new Vector2(positionX, positionY);
                    GameObject instA = Instantiate(EnemyGroupA.TypeOfShip, spawnPosition, spawnRotation);
                    if (counter == 0)
                    {
                        
                        currentWaveKillA = 0;
                        
                        currentWaveA = new int[EnemyGroupA.NoOfShips];
                        currentWaveA[counter] = instA.GetInstanceID();
                        //someid = currentWaveA[counter];
                        counter++;
                    }
                    while (left >= 0 || right < EnemyGroupA.NoOfShips)
                    {
                        yield return new WaitForSeconds(EnemyGroupA.NextShipSec);

                        if (left >= 0)
                        {
                            lx = positionX + (leader - left) * EnemyGroupA.hDistance;
                            spawnPosition = new Vector2(lx, positionY);
                             GameObject instAL = Instantiate(EnemyGroupA.TypeOfShip, spawnPosition, spawnRotation);
                            currentWaveA[counter] = instAL.GetInstanceID();
                            //someid = currentWaveA[counter];
                            counter++;
                            if (counter == EnemyGroupA.NoOfShips)
                            {
                                lastWaveA = new int[counter];
                                for (int i = 0; i < EnemyGroupA.NoOfShips; i++)
                                {
                                    lastWaveA[i] = currentWaveA[i];

                                }
                                lastWaveKillA = currentWaveKillA;
                            }
                        }
                        if (right < EnemyGroupA.NoOfShips)
                        {
                            rx = positionX + (leader - right) * EnemyGroupA.hDistance;
                            spawnPosition = new Vector2(rx, positionY);
                            GameObject instAR = Instantiate(EnemyGroupA.TypeOfShip, spawnPosition, spawnRotation);
                            currentWaveA[counter] = instAR.GetInstanceID();
                            //someid = currentWaveA[counter];
                            counter++;
                            if (counter == EnemyGroupA.NoOfShips)
                            {
                                lastWaveA = new int[counter];
                                for (int i = 0; i < EnemyGroupA.NoOfShips; i++)
                                {
                                    lastWaveA[i] = currentWaveA[i];

                                }
                                lastWaveKillA = currentWaveKillA;
                            }
                        }

                        left--;
                        right++;
                    }

                    //set next spawn for enemy B
                    isA = !isA;
                    yield return new WaitForSeconds(NextSpawnWaveSec);
                }
                else
                {
                    float positionX = Random.Range(-EnemyGroupB.spawnValues.x, EnemyGroupB.spawnValues.x);

                    float positionY = EnemyGroupB.spawnValues.y;


                    for (int i = 0; i < EnemyGroupB.NoOfShips; i++)
                    {
                        spawnPosition = new Vector2(positionX, positionY);
                        //Instantiate(EnemyGroupB.TypeOfShip, spawnPosition, spawnRotation);
                        GameObject instB = Instantiate(EnemyGroupB.TypeOfShip, spawnPosition, spawnRotation);
                        if (i == 0)
                        {

                            currentWaveKillB = 0;

                            currentWaveB = new int[EnemyGroupB.NoOfShips];
                            //currentWaveB[i] = instB.GetInstanceID();
                            //someid = currentWaveA[counter];
                            
                        }
                        currentWaveB[i] = instB.GetInstanceID();
                        if (i == EnemyGroupB.NoOfShips - 1)
                        {
                            lastWaveB = new int[EnemyGroupB.NoOfShips];
                            for (int j = 0; j < EnemyGroupB.NoOfShips; j++)
                            {
                                lastWaveB[j] = currentWaveB[j];

                            }
                            lastWaveKillB = currentWaveKillB;
                        }
                        if (i < EnemyGroupB.NoOfShips - 1)
                        {
                            yield return new WaitForSeconds(EnemyGroupB.NextShipSec);
                        }

                    }

                    //set next spawn for enemy A
                    isA = !isA;
                    yield return new WaitForSeconds(NextSpawnWaveSec);
                }

                wCounter++;
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }


            yield return new WaitForSeconds(bossComing);

                spawnPosition = new Vector2(0.0f, spawnPosBoss);
                Instantiate(bossA, spawnPosition, spawnRotation);
                isBossFight = true;
               
            
            
        }
    }

    public  bool onBossFight()
    {
        return isBossFight;
    }

   

    public bool checkLastWaveA(int id)
    {
        for(int i=0;i<EnemyGroupA.NoOfShips;i++)
        {
            if (id == lastWaveA[i])
            {
                //someid = lastWaveA[i];
                lastWaveKillA++;
                return true;
            }
            
        }
        return false;
    }

    public bool checkCurrentWaveA(int id)
    {
        for (int i = 0; i < EnemyGroupA.NoOfShips; i++)
        {
            if (id == currentWaveA[i])
            {
                //someid = currentWaveA[i];
                currentWaveKillA++;
                return true;
            }

        }
        return false;
    }

    public bool checkLastWaveKillA()
    {
        if(lastWaveKillA == EnemyGroupA.NoOfShips)
        {
            lastWaveKillA = 0;
            return true;
        }
        return false;
    }

    public bool checkLastWaveB(int id)
    {
        for (int i = 0; i < EnemyGroupB.NoOfShips; i++)
        {
            if (id == lastWaveB[i])
            {
                //someid = lastWaveA[i];
                lastWaveKillB++;
                return true;
            }

        }
        return false;
    }

    public bool checkCurrentWaveB(int id)
    {
        for (int i = 0; i < EnemyGroupB.NoOfShips; i++)
        {
            if (id == currentWaveB[i])
            {
                //someid = currentWaveA[i];
                currentWaveKillB++;
                return true;
            }

        }
        return false;
    }

    public bool checkLastWaveKillB()
    {
        if (lastWaveKillB == EnemyGroupB.NoOfShips)
        {
            lastWaveKillB = 0;
            return true;
        }
        return false;
    }

    public void dropItem(Transform trans)
    {
        Instantiate(powerUp, trans.position, trans.rotation);
    }

    public void AddScore(int reward)
    {
        Score += reward;
        UpdateScore();
    }

    void UpdateScore() 
    {
        ScoreText.text = "Score: " + Score;
    }

    public void reLoop()

    {
        if(!isBulletHell)
        {
            GameOver();
        }
        Looping = true;
    }
    public void UpdateLevel(Player _p)
    {
        playerLv.text = "Lv. " + _p.checkCurrentLvl();
    }

    public void UpdateBossHP(BossA b)
    {
        if (b.DisplayHP() > 0)
        {
            bossHP.text = "Boss HP " + b.DisplayHP();

        }
        else
        {
            bossHP.text = "";

            reLoop();
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public float getBGOffsetScrollSpeed()
    {
        return currentScrollSpeed;
    }

    private int v_leaderGenerator()
    {

        int leaderNo = Random.Range(1, 4);
        return leaderNo;
    }


}
