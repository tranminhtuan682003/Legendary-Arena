using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnightController : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        InitLize();
    }

    private void Update()
    {
        HandleMove();
    }

    private void InitLize()
    {
        speed = 20f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMove()
    {
        rb.velocity = transform.right * speed;
    }


    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var knight = other.GetComponent<KnightController>();
        if (other.CompareTag("Knight"))
        {
            knight.TakeDamage(100);
        }
    }
}
