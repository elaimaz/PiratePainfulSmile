using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 0.8f;

    [SerializeField]
    private GameObject explosionAnimation = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<IDamageble>().Damage(1);
            Instantiate(explosionAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
