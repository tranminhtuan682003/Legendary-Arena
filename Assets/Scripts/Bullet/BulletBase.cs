using System.Collections;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    private Rigidbody rb;
    private float speedMove;
    private int damage;
    private Transform target;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleMove();
    }

    public void Initialize(float speedMove, Transform target, int damage)
    {
        this.speedMove = speedMove;
        this.target = target;
        this.damage = damage;
    }

    private void HandleMove()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedMove * Time.deltaTime);
        }
        else
        {
            rb.velocity = transform.forward * speedMove * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ITeamMember targetMember = other.GetComponent<ITeamMember>();

        if (targetMember != null && other.transform == target)
        {
            ApplyDamage(other);
            gameObject.SetActive(false);
        }
    }

    private void ApplyDamage(Collider other)
    {
        if (other.TryGetComponent<HeroBase>(out var player))
        {
            player.TakeDamage(damage);
        }
        else if (other.TryGetComponent<SoldierBase>(out var soldier))
        {
            soldier.TakeDamage(damage);
        }
        else if (other.TryGetComponent<TurretBase>(out var turret))
        {
            turret.TakeDamage(damage);
        }
    }
}
