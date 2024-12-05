using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnightController : MonoBehaviour
{
    private float speed = 25f;
    private int damage = 150;
    private Rigidbody2D rb;
    private GameObject target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Nếu có mục tiêu, bắt đầu di chuyển về phía mục tiêu
        if (target != null)
        {
            StartCoroutine(MoveTowardsTarget());
        }
        else
        {
            // Nếu không có target, di chuyển theo hướng mặc định
            rb.linearVelocity = transform.right * speed;
        }
    }

    public void InitLize(GameObject target)
    {
        this.target = target;

        // Nếu có target, bắt đầu di chuyển về phía mục tiêu
        if (target != null)
        {
            StartCoroutine(MoveTowardsTarget());
        }
        else
        {
            // Nếu không có target, di chuyển theo hướng mặc định
            rb.linearVelocity = transform.right * speed;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false); // Tắt viên đạn khi nó ra khỏi màn hình
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra va chạm với một đối tượng kế thừa từ ITeamMember
        var teamMember = other.GetComponent<ITeamMember>();
        if (teamMember != null)
        {
            if (teamMember.GetTeam() == Team.Red)
            {
                teamMember.TakeDamage(damage);
                gameObject.SetActive(false); // Tắt viên đạn
            }
        }
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (target != null)
        {
            // Tính hướng di chuyển về phía mục tiêu
            Vector2 direction = (target.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
            yield return null; // Đợi một frame trước khi tiếp tục
        }

        // Nếu không còn target, di chuyển theo hướng mặc định
        rb.linearVelocity = transform.right * speed;
    }
}
