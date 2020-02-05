using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed;

    [SerializeField]
    Vector2 edge;

    [SerializeField]
    int wrapTime;


    //public GameObject Hit;
    private GameController gameController;



    Rigidbody2D rigidbody;
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

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity =  transform.up * speed;
        
	}



    private void Update()
    {
        if(gameController.isBulletHell && wrapTime > 0)
        {
            if(rigidbody.position.y > edge.y)
            {
                rigidbody.position = new Vector2(rigidbody.position.x, -edge.y);
                wrapTime--;
            }

            if (rigidbody.position.y < -edge.y)
            {
                rigidbody.position = new Vector2(rigidbody.position.x, edge.y);
                wrapTime--;
            }

        }
    }

    //bullet cancellation
    //void OnTriggerEnter2D(Collider2D other)
    //{

    //    //active only from enemy bullet, avoid duplication
    //    if ((other.tag == "Bullet" && this.tag == "BulletE"))
    //    {
    //        Instantiate(Hit, transform.position, transform.rotation);
    //        Instantiate(Hit, other.transform.position, other.transform.rotation);

    //        Destroy(other.gameObject);
    //        Destroy(gameObject);
    //    }

    //}

}
