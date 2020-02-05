using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShipDBC : MonoBehaviour {

    
    public GameObject explosion;
    
    public GameObject playerExplosion;

    public GameObject Hit;

    public int scoreValue;

    private GameController gameController;

    public int enemyType;


    void Start()
    {
        GameObject gameControllerObj = GameObject.FindWithTag("GameController");

        if(gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boundary")
        {
            return;
        }





        if (other.tag == "Bullet" || other.tag == "Player" )
        {
                Instantiate(Hit, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, transform.rotation);


            int id = gameObject.GetInstanceID();

            gameController.AddScore(scoreValue);
            if (enemyType == 1)
            {
                if (gameController.checkLastWaveA(id))
                {
                    if (gameController.checkLastWaveKillA())
                    {
                        gameController.AddScore(scoreValue * 5);
                        gameController.dropItem(gameObject.transform);
                    }
                }
                else
                {
                    gameController.checkCurrentWaveA(id);

                }
            }
            if (enemyType == 2)
            {
                if (gameController.checkLastWaveB(id))
                {
                    if (gameController.checkLastWaveKillB())
                    {
                        gameController.AddScore(scoreValue * 5);
                        gameController.dropItem(gameObject.transform);
                    }
                }
                else
                {
                    gameController.checkCurrentWaveB(id);

                }
            }
            if (other.tag == "Bullet")
            {
                Destroy(other.gameObject);

            }
            Destroy(gameObject);
           
            
            
        }
    }
}
