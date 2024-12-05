using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnightController : MonoBehaviour
{
    private float speed = 25f;
    private int damage = 150;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = transform.right * speed;
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
        var targetTeamMember = other.GetComponent<ITeamMember>();
        if (targetTeamMember != null && targetTeamMember.GetTeam() == Team.Red)
        {
            ApplyDamage(targetTeamMember);
            // OnBulletHit();
        }
    }

    protected virtual void ApplyDamage(ITeamMember targetTeamMember)
    {
        targetTeamMember.TakeDamage(damage);
    }

    protected virtual void OnBulletHit()
    {
        gameObject.SetActive(false);
    }
}
