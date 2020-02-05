using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour {

    //[SerializeField]
    //float PosY;

    [SerializeField]
    float Speed;

    [SerializeField]
    float PeriodInFrame;

    [SerializeField]
    float AmplitudeR;

    [SerializeField]
    Vector2 MoveArea;

    //private bool isTheLast;
    private float X;
    private float Y;
    float angSpeed;
   // float x;
    Vector2 pos;
    
    Vector2 direction;

    Rigidbody2D rigidbody;

    float dist;

    //float angOffset;

    float lastMoment;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        pos.x = rigidbody.position.x;
        pos.y = rigidbody.position.y;
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
        //angOffset = Mathf.Abs(Mathf.Asin(dist / MoveArea.x));
        //rigidbody.velocity = -transform.up * Speed* direction.y;
        Update();
    }
	
	// Update is called once per frame
	void Update () {

       
        Y =  direction.y*Speed * Time.deltaTime;
        X = direction.x * Speed * Time.deltaTime;

        checkDirect();
        transform.position = new Vector2(rigidbody.position.x + X, rigidbody.position.y + Y);
        
    }

    private void checkDirect()
    {
        if(rigidbody.position.y >= MoveArea.y && direction.y >= 0)
        {

            direction.y = -1;

        }
        if(rigidbody.position.y <-MoveArea.y && direction.y < 0)
        {
            direction.y = 1;
        }
        if (rigidbody.position.x >= MoveArea.x && direction.x >= 0)
        {

            direction.x = -1;

        }
        if (rigidbody.position.x < -MoveArea.x && direction.x < 0)
        {
            direction.x = 1;
        }
    }
    
}
