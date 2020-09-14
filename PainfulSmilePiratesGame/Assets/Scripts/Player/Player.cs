using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Text healthText = null;

    private GameManager gameManager = null;

    [SerializeField]
    private GameObject explosionDeath = null;

    [SerializeField]
    private GameObject[] shipSpritesToBeHidden = null;
    [SerializeField]
    private Sprite[] shipSprites = null;
    private SpriteRenderer spriteRenderer;
    private int MaxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthText.text = health.ToString();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = shipSprites[0];
        MaxHealth = health;
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
            healthText.text = health.ToString();
            ChangeSprite();
            if (health < 1)
                Death();
        }
    }

    public void ChangeSprite()
    {
        int healthPercentage = Mathf.RoundToInt((float)health / MaxHealth * 100); 
        if (healthPercentage <= 67 && healthPercentage > 33)
            spriteRenderer.sprite = shipSprites[1];
        else if (healthPercentage <= 33)
            spriteRenderer.sprite = shipSprites[2];
    }

    public void Death()
    {
        for (int i = 0; i < shipSpritesToBeHidden.Length; i++)
        {
            shipSpritesToBeHidden[i].SetActive(false);
        }
        StartCoroutine(WaitDeathAnimation());
        Instantiate(explosionDeath, transform.position, Quaternion.identity);
    }

    private IEnumerator WaitDeathAnimation()
    {
        yield return new WaitForSeconds(2);
        gameManager.playerAlive = false;
        Destroy(gameObject);
    }
}
