using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTowerRedController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int damage;
    private GameObject enemy; // Mục tiêu của đạn
    private float bulletSpeed; // Tốc độ đạn

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Nếu có mục tiêu, di chuyển viên đạn về phía mục tiêu
        if (enemy != null)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    // Hàm khởi tạo đạn
    public void Initialize(GameObject target, float speed, int bulletDamage)
    {
        enemy = target;
        bulletSpeed = speed;
        damage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<KnightController>();
        if (target != null && target.GetTeam() == Team.Blue) // Kiểm tra team
        {
            target.TakeDamage(damage); // Gây sát thương cho mục tiêu
            gameObject.SetActive(false); // Tắt đạn
        }
    }
}
