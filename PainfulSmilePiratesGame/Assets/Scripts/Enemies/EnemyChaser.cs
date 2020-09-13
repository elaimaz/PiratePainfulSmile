using UnityEngine;

public class EnemyChaser : MonoBehaviour, IDamageble
{
    private Rigidbody2D rb;
    private Transform playerPosition;
    private Vector3 direction;
    private float angle;
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private int health = 2;

    private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        //rb.AddForce(direction * speed);
    }

    public void Damage(int damageDone)
    {
        health -= damageDone;
        if (health < 1)
            Death();
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
