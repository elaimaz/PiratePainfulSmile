﻿using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    //Move Variables
    private Rigidbody2D rb;
    private float thrust = 0;
    [SerializeField]
    private float shipSpeed = 1f;
    private float shipRotation = 0;
    [SerializeField]
    private float rotationRate = 30;

    //Attack Variables
    [SerializeField]
    private float singleShotRate = 1f;
    private float lastSingleShotTime = 0;
    [SerializeField]
    private float tripleShotRate = 1.5f;
    private float lastTripeShotTime = 0;
    [SerializeField]
    private GameObject cannonBall = null;
    [SerializeField]
    private GameObject frontCannon = null;
    [SerializeField]
    private GameObject rightCannonBall = null;
    [SerializeField]
    private GameObject rightCannon1 = null;
    [SerializeField]
    private GameObject rightCannon2 = null;
    [SerializeField]
    private GameObject rightCannon3 = null;

    //Damage Varibles
    [SerializeField]
    private int health = 7;
    [SerializeField]
    private float damageRate = 1.5f;
    private float lastDamageTime = 0;
    
    public bool canBeDamageble = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrust = Input.GetAxis("Vertical");
        shipRotation = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Fire1"))
            SingleShoot();

        if (Input.GetAxis("Fire2") > 0)
            TripleShoot();

        if (Time.time > damageRate + lastDamageTime)
            canBeDamageble = true;
        else
            canBeDamageble = false;
    }

    private void FixedUpdate()
    {
        if (thrust > 0)
            rb.AddForce(-transform.right * shipSpeed);

        if (shipRotation != 0)
            transform.Rotate(-Vector3.forward * shipRotation * rotationRate * Time.deltaTime);    
    }

    private void SingleShoot()
    {
        if(Time.time > singleShotRate + lastSingleShotTime)
        {
            lastSingleShotTime = Time.time;
            Instantiate(cannonBall, frontCannon.transform.position, transform.rotation);
        }
    }

    private void TripleShoot()
    {
        if (Time.time > tripleShotRate + lastTripeShotTime)
        {
            lastTripeShotTime = Time.time;
            Instantiate(rightCannonBall, rightCannon1.transform.position, transform.rotation);
            Instantiate(rightCannonBall, rightCannon2.transform.position, transform.rotation);
            Instantiate(rightCannonBall, rightCannon3.transform.position, transform.rotation);
        }
    }

    public void Damage(int damageDone)
    {
        if (canBeDamageble == true)
        {
            lastDamageTime = Time.time;
            health -= damageDone;
            if (health < 1)
                Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}