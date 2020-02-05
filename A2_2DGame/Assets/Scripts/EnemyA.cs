using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour {

    [SerializeField]
    float speed;


    Rigidbody2D rigidbody;
    // Use this for initialization
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * speed;
    }

   
}
