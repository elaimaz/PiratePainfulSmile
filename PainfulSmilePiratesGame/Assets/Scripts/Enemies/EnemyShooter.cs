using UnityEngine;
using UnityEngine.UI;

public class EnemyShooter : MonoBehaviour, IDamageble
{
    //Rotation Move
    private Rigidbody2D rb;
    private Transform playerPosition;
    private Vector3 direction;
    private float angle;

    //Health
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private Text healthText = null;

    //Attack
    [SerializeField]
    private float ShootRate = 3f;
    private float lastShoot = 0;
    [SerializeField]
    private GameObject enemyCannonBall = null;
    [SerializeField]
    private Transform cannonR = null;
    [SerializeField]
    private Transform cannonL = null;
    public float distance;

    private GameManager gameManager;

    [SerializeField]
    private Sprite[] shipSprites = null;
    private SpriteRenderer spriteRenderer;
    private int MaxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastShoot = Time.time;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        healthText.text = health.ToString();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = shipSprites[0];
        MaxHealth = health;
    }

    private void Update()
    {
        if (playerPosition)
        {
            direction = playerPosition.position - transform.position;
            direction.Normalize();
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            distance = Vector3.Distance(transform.position, playerPosition.transform.position);
        }
        
        if (Time.time > ShootRate + lastShoot && distance < 6.0f)
        {
            lastShoot = Time.time;
            Shoot();
        }

        if (distance > 25)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.rotation = angle;
    }

    private void Shoot()
    {
        Instantiate(enemyCannonBall, cannonR.transform.position, transform.rotation);
        Instantiate(enemyCannonBall, cannonL.transform.position, transform.rotation);
    }

    public void Damage(int damageDone)
    {
        health -= damageDone;
        healthText.text = health.ToString();
        ChangeSprite();
        if (health < 1)
            Death();
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
        gameManager.UpdateScore();
        Destroy(gameObject);
    }
}
