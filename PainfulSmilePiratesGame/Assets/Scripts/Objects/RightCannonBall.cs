﻿using UnityEngine;

public class RightCannonBall : MonoBehaviour
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
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }
}