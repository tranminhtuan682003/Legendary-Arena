using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletTowerKnightBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected int damage;
    protected GameObject target; // Mục tiêu của đạn
    protected float bulletSpeed; // Tốc độ đạn
    protected Vector2 defaultDirection = Vector2.right; // Hướng mặc định (có thể thay đổi theo yêu cầu)

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Di chuyển về hướng của target
    protected virtual void MoveTowardsTarget()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed; // Di chuyển về target
    }

    // Di chuyển theo hướng mặc định nếu không có target
    protected virtual void MoveInDefaultDirection()
    {
        rb.linearVelocity = defaultDirection * bulletSpeed; // Di chuyển theo hướng mặc định
    }

    public virtual void Initialize(GameObject target, float speed, int bulletDamage)
    {
        this.target = target;
        this.bulletSpeed = speed;
        this.damage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var targetTeamMember = other.GetComponent<ITeamMember>();
        if (targetTeamMember != null && ShouldDamage(targetTeamMember))
        {
            ApplyDamage(targetTeamMember);
            OnBulletHit();
        }
    }

    protected abstract bool ShouldDamage(ITeamMember targetTeamMember);

    protected virtual void ApplyDamage(ITeamMember targetTeamMember)
    {
        targetTeamMember.TakeDamage(damage);
    }

    protected virtual void OnBulletHit()
    {
        gameObject.SetActive(false);
    }
}
