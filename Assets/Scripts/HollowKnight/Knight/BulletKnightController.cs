using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnightController : MonoBehaviour
{
    private float speed = 25f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knight"))
        {
            var knight = other.GetComponent<KnightController>();
            if (knight != null)
            {
                knight.TakeDamage(100);
            }

            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }
}
