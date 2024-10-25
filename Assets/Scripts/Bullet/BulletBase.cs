using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    private Rigidbody rb;
    private float speedMove;
    private int damage;
    private Transform target;

    // Tập hợp các tag địch để linh hoạt hơn
    private Dictionary<string, System.Action<Collider>> enemyHandlers;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleMove();
    }

    protected virtual void Initialize(string tagEnemy, string tagSoldierEnemy, string tagTurretEnemy, float speedMove, Transform target, int damage)
    {
        this.speedMove = speedMove;
        this.target = target;
        this.damage = damage;

        enemyHandlers = new Dictionary<string, System.Action<Collider>>
        {
            { tagEnemy, HandlePlayerHit },
            { tagSoldierEnemy, HandleSoldierHit },
            { tagTurretEnemy, HandleTurretHit }
        };
    }

    private void HandleMove()
    {
        if (target == null)
        {
            rb.velocity = transform.forward * speedMove * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speedMove * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyHandlers.TryGetValue(other.tag, out System.Action<Collider> handler))
        {
            handler.Invoke(other);
        }
    }

    private void HandlePlayerHit(Collider other)
    {
        IhealthPlayer player = other.GetComponent<IhealthPlayer>();
        if (player != null)
        {
            player.TakeDamage(1000f);
            gameObject.SetActive(false);
        }
    }

    private void HandleSoldierHit(Collider other)
    {
        SoldierBase soldierEnemy = other.GetComponent<SoldierBase>();
        if (soldierEnemy != null)
        {
            soldierEnemy.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    private void HandleTurretHit(Collider other)
    {
        TurretBase turretBase = other.GetComponent<TurretBase>();
        if (turretBase != null)
        {
            turretBase.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
