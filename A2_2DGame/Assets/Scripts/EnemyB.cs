using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour {

    [SerializeField]
    float FireStartPosY;

    [SerializeField]
    float Speed;

    [SerializeField]
    float PeriodInFrame;

    [SerializeField]
    float AmplitudeR;

    [SerializeField]
    float AttackArea;

    [SerializeField]
    float FireRate;
    float NextFire;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform Cannon;


    //private bool isTheLast;
    private float minX;
    private float maxX;
    float angSpeed;
    float x;
    Vector2 pos;
    float lastMoment;
    int direction;


    Rigidbody2D rigidbody;
    //GameObject player;
    //Rigidbody2D pBody;

    // Use this for initialization
    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        minX = rigidbody.position.x - AttackArea;
        maxX = rigidbody.position.x + AttackArea;
        lastMoment = Time.time;
        angSpeed = 360.0f / PeriodInFrame;
        if (rigidbody.position.x > 0)
        {
            direction = 1;
        }
        if (rigidbody.position.x < 0 )
        {
            direction = -1;
        }
        pos = transform.position;
        rigidbody.velocity = - transform.up * Speed;
        
        Update();


    }


    void Update()
    {
        x = direction * AmplitudeR * Mathf.Sin(angSpeed * (Time.time - lastMoment));
        //rigidbody.position = new Vector2(x, rigidbody.position.y);
        transform.position = new Vector2(pos.x + x, rigidbody.position.y);


        if (atkAvailable() && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            Instantiate(Bullet, Cannon.position, Cannon.rotation);
            //GameObject bulletObj = Instantiate(shot, cannon.position, cannon.rotation);
            //Bullet bullet = bulletObj.GetComponent<Bullet>();
            //FireSound.Play();

        }
    }

    

    //public void limitation(float _minX, float _maxX)
    //{
    //    minX = _minX;
    //    maxX = _maxX;
    //}

    private bool atkAvailable()
    {
        if (rigidbody.position.x < maxX && 
            rigidbody.position.x > minX && 
            rigidbody.position.y < FireStartPosY 
            )
        {
            return true;
        }
        else
            return false;



    }


}
