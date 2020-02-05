using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDBCE : MonoBehaviour
{

    public GameObject Hit;

    
    

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "PowerUp" || other.tag =="BulletE")
        {
            return;
        }





        if ((other.tag == "Bullet") || (other.tag == "Player"))
            
        {


            Instantiate(Hit, transform.position, transform.rotation);
           
            
            if (other.tag == "Bullet")
                Destroy(other.gameObject);
            else
            {
                if(other.tag == "Player")
                {
                    return;
                }

            }
            Destroy(gameObject);
        }
    }
}
