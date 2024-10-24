using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Collections;
using System;
public abstract class TurretBase : MonoBehaviour
{
    private IState currentState;
    //ScriptTableObject SupplyToTheTurret
    private SupplyToTheTurret itemOfTurret;
    private const string ItemAddress = "Assets/Scripts/Tower/ItemOfTurret.asset";
    private GameObject bulletTurret;
    private Transform spawnPoint;

    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; }
    public bool IsActive => CurrentHealth > 0;
    public float DetectionRange { get; protected set; }
    public float AttackRange { get; protected set; }
    public int AttackDamage { get; protected set; }
    public float AttackInterval { get; protected set; } = 2f; // Thời gian giữa các đòn tấn công

    private Transform target;
    private float attackCooldown = 0f;

    protected virtual void Start()
    {
        currentState = new TurretIdleState(this);
        currentState.Enter();
        InitItem();
    }

    private void Update()
    {
        currentState?.Execute();
        DetectPlayer();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual void Initialize(int maxHealth, float detectionRange, float attackRange, int attackDamage)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        DetectionRange = detectionRange;
        AttackRange = attackRange;
        AttackDamage = attackDamage;
    }

    private void InitItem()
    {
        spawnPoint = transform.Find("SpawnPoint");
        Addressables.LoadAssetAsync<SupplyToTheTurret>(ItemAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                itemOfTurret = handle.Result;
                bulletTurret = GetPrefabByName("BulletTurret");
            }
        };
    }
    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in itemOfTurret.items)
        {
            if (item.name == name)
            {
                return item.prefab;
            }
        }

        Debug.LogWarning("Prefab not found!");
        return null;
    }

    public virtual void ReduceDamage(int percentage)
    {
        int damageReduction = (percentage * CurrentHealth) / 100;
        CurrentHealth -= damageReduction;
    }

    public virtual void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            ChangeState(new TurretDestroyedState(this));
        }
    }

    private void OnDrawGizmos()
    {
        if (DetectionRange > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, DetectionRange);
        }
    }
    public void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
        bool playerDetected = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                target = hitCollider.transform;
                GamePlay1vs1Manager.Instance.target = target;
                playerDetected = true;

                if (Vector3.Distance(transform.position, target.position) <= AttackRange)
                {
                    if (!(currentState is TurretAttackState))
                    {
                        ChangeState(new TurretAttackState(this));
                    }
                }
                else
                {
                    if (!(currentState is TurretIdleState))
                    {
                        ChangeState(new TurretIdleState(this));
                    }
                }
                break;
            }
        }

        if (!playerDetected && !(currentState is TurretIdleState))
        {
            ChangeState(new TurretIdleState(this));
            target = null;
        }
    }



    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            Debug.Log("Turret is attacking " + target.name);
            ObjectPool.Instance.GetFromPool(bulletTurret, spawnPoint.position, spawnPoint.rotation);
            attackCooldown = AttackInterval;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void ShowAttackRange(bool show)
    {
        if (show)
        {
            Debug.Log("Displaying attack range.");
        }
        else
        {
            Debug.Log("Hiding attack range.");
        }
    }

    protected abstract void AttackTarget(HeroBase target);
}
