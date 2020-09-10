using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float thrust = 0;
    [SerializeField]
    private float shipSpeed = 1;
    private float shipRotation = 0;
    [SerializeField]
    private float rotationRate = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        thrust = Input.GetAxis("Vertical");
        shipRotation = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (thrust > 0)
            rb.AddForce(-transform.right * thrust * shipSpeed);

        if (shipRotation != 0)
            transform.Rotate(-Vector3.forward * shipRotation * rotationRate * Time.deltaTime);    
    }
}
