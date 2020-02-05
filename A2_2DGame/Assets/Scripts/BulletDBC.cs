using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDBC : MonoBehaviour {

    public GameObject Hit;






    void OnTriggerEnter2D(Collider2D other)
    {
        




        if ( (other.tag == "Player"))

        {


            Instantiate(Hit, transform.position, transform.rotation);

            

            return;
            
            
        }
    }
}
