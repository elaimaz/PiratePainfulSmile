using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
}
