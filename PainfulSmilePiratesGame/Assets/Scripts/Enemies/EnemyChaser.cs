using UnityEngine;
using UnityEngine.UI;

public class EnemyChaser : MonoBehaviour, IDamageble
{
    private Rigidbody2D rb;
    private Transform playerPosition;
    private Vector3 direction;
    private float angle;

    [Header("Enemy Health")]
    [SerializeField]
    private int health = 2;
    [SerializeField]
    private Text healthText = null;

    private GameManager gameManager;

    [Header("VFX")]
    [SerializeField]
    private Sprite[] shipSprites = null;
    private SpriteRenderer spriteRenderer;
    private int MaxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        }
    }

    private void FixedUpdate()
    {
        rb.rotation = angle;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().canBeDamageble == true)
            {
                collision.GetComponent<Player>().Damage(1);
                Death();
            }
        }
    }
}
