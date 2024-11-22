using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnightController : MonoBehaviour
{
    private float speed = 25f;
    private int damage = 100;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.right * speed;
    }

    public void InitLize(int damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tower = other.GetComponent<IEnemy>();
        if (other.CompareTag("TowerRed"))
        {
            tower.TakeDamage(damage);
        }
    }
}
