using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAContact : MonoBehaviour {

    //public GameObject explosion;

    

    public BossA BossA;
    public GameObject Hit;

    //public int scoreValue;

    private GameController gameController;





    void Start()
    {
        GameObject gameControllerObj = GameObject.FindWithTag("GameController");

        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }

        gameController.UpdateBossHP(BossA);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }





        if (other.tag == "Bullet" || other.tag == "Player")
        {
            if (this.tag == "BossPartB")
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(Hit, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);

                }

            }
            if (this.tag == "BossPartAL")
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(Hit, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);

                }
                this.BossA.LSGTakenDMG();
               
            }
            if (this.tag == "BossPartAR")
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(Hit, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);

                }
                this.BossA.RSGTakenDMG();

            }

            if (this.tag == "BossPartC")
            {
                if (other.tag == "Bullet")
                {
                    Instantiate(Hit, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);

                }
                this.BossA.CoreTakenDMG();
                gameController.UpdateBossHP(BossA);


            }






        }
    }
}
