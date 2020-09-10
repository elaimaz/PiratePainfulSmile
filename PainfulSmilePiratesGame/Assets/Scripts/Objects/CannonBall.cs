﻿using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 0.8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
    }
}
