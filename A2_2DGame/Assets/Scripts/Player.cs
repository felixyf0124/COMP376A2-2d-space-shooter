using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}



public class Player : MonoBehaviour {

    [SerializeField]
    float mMoveSpeed = 1.2f;

    

    Rigidbody2D mRigidBody2D;
    
    public Boundary mBoundary;

    [SerializeField]
    GameObject shot;
    [SerializeField]
    Transform Cannon0;
    [SerializeField]
    Transform CannonL1;
    [SerializeField]
    Transform CannonR1;
    [SerializeField]
    Transform CannonL2;
    [SerializeField]
    Transform CannonR2;
    [SerializeField]
    Transform CannonL3;
    [SerializeField]
    Transform CannonR3;


    [SerializeField]
    float fireRate;
    float nextFire;


    public int Level = 1;
    public int maxLevel = 7;
    private int minlv;
    //public AudioSource[] Audio[2];

    public AudioSource fireSound;
    public AudioSource Power;
    
    // Use this for initialization
    void Start () {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        //Audio = GetComponents<AudioSource>();
        //fireSound = Audio[0];
        //Power = Audio[1];
        
        minlv = 0;
        //FixedUpdate();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fireSound.Play();
            fire();
            
            
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        mRigidBody2D.velocity = movement * mMoveSpeed;


        mRigidBody2D.position = new Vector2(Mathf.Clamp(mRigidBody2D.position.x, mBoundary.xMin, mBoundary.xMax),
                                             Mathf.Clamp(mRigidBody2D.position.y, mBoundary.yMin, mBoundary.yMax));

        
	}

    public void Damaged()
    {
        if (Level > minlv)
        {
            Level--;

        }


    }
    public void Boost()
    {
        if (Level < maxLevel)
        {
            Level++;
            Power.Play();
        }
    }

    

    public int checkCurrentLvl()
    {
        return Level;
    }



    //fire at certain level
    private void fire()
    {
        if (Level == 1)
            Instantiate(shot, Cannon0.position, Cannon0.rotation);
        if (Level == 2)
        {
            Instantiate(shot, CannonL1.position, CannonL1.rotation);
            Instantiate(shot, CannonR1.position, CannonR1.rotation);
        }
        if (Level == 3)
        {
            Instantiate(shot, Cannon0.position, Cannon0.rotation);
            Instantiate(shot, CannonL2.position, CannonL2.rotation);
            Instantiate(shot, CannonR2.position, CannonR2.rotation);
        }
        if (Level == 4)
        {
            Instantiate(shot, CannonL1.position, CannonL1.rotation);
            Instantiate(shot, CannonR1.position, CannonR1.rotation);
            Instantiate(shot, CannonL2.position, CannonL2.rotation);
            Instantiate(shot, CannonR2.position, CannonR2.rotation);
        }
        if (Level == 5)
        {
            Instantiate(shot, Cannon0.position, Cannon0.rotation);
            Instantiate(shot, CannonL1.position, CannonL1.rotation);
            Instantiate(shot, CannonR1.position, CannonR1.rotation);
            Instantiate(shot, CannonL2.position, CannonL2.rotation);
            Instantiate(shot, CannonR2.position, CannonR2.rotation);
        }
        if (Level == 6)
        {
            Instantiate(shot, CannonL1.position, CannonL1.rotation);
            Instantiate(shot, CannonR1.position, CannonR1.rotation);
            Instantiate(shot, CannonL2.position, CannonL2.rotation);
            Instantiate(shot, CannonR2.position, CannonR2.rotation);
            Instantiate(shot, CannonL3.position, CannonL3.rotation);
            Instantiate(shot, CannonR3.position, CannonR3.rotation);
        }
        if (Level == 7)
        {
            Instantiate(shot, Cannon0.position, Cannon0.rotation);
            Instantiate(shot, CannonL1.position, CannonL1.rotation);
            Instantiate(shot, CannonR1.position, CannonR1.rotation);
            Instantiate(shot, CannonL2.position, CannonL2.rotation);
            Instantiate(shot, CannonR2.position, CannonR2.rotation);
            Instantiate(shot, CannonL3.position, CannonL3.rotation);
            Instantiate(shot, CannonR3.position, CannonR3.rotation);
        }
    }

}
