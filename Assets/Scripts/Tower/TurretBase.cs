using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour
{
    private IState currentState;
    private SupplyToTheTurret turretDatabase;
    private const string TurretDatabaseAddress = "Assets/Scripts/Tower/ItemOfTurret.asset";
    private GameObject bulletTurret;
    private Transform spawnPoint;

    // Các thuộc tính
    private int currentHealth;
    private int maxHealth;
    private float detectionRange;
    private float attackRange;
    private int attackDamage;
    private float attackInterval = 2f;

    private Transform target;
    private float attackCooldown = 0f;

    // Dictionary ánh xạ tag với các hàm xử lý mục tiêu
    private Dictionary<string, System.Action<Collider>> targetHandlers;

    protected virtual void Start()
    {
        currentState = new TurretIdleState(this);
        currentState.Enter();
        InitItem();
    }

    protected virtual void Update()
    {
        currentState?.Execute();
        DetectTarget();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual void Initialize(int maxHealth, float detectionRange, float attackRange, int attackDamage, string tagEnemy, string tagSoldierEnemy)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;

        targetHandlers = new Dictionary<string, System.Action<Collider>>
        {
            { tagEnemy, HandlePlayerTarget },
            { tagSoldierEnemy, HandleSoldierTarget }
        };
    }

    private void InitItem()
    {
        spawnPoint = transform.Find("SpawnPoint");
        Addressables.LoadAssetAsync<SupplyToTheTurret>(TurretDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                turretDatabase = handle.Result;
                bulletTurret = GetPrefabByName("BulletTurret");
            }
        };
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in turretDatabase.items)
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
        int damageReduction = (percentage * currentHealth) / 100;
        currentHealth -= damageReduction;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            ChangeState(new TurretDestroyedState(this));
        }
    }

    public void DetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (var hitCollider in hitColliders)
        {
            if (targetHandlers.TryGetValue(hitCollider.tag, out var handler))
            {
                target = hitCollider.transform;
                GamePlay1vs1Manager.Instance.targetOfTurret = target;
                handler.Invoke(hitCollider);
                return;
            }
        }

        if (!(currentState is TurretIdleState))
        {
            ChangeState(new TurretIdleState(this));
            target = null;
        }
    }

    private void HandlePlayerTarget(Collider other)
    {
        UpdateStateBasedOnTarget();
    }

    private void HandleSoldierTarget(Collider other)
    {
        UpdateStateBasedOnTarget();
    }

    private void UpdateStateBasedOnTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
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
    }

    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            Debug.Log("Turret is attacking " + target.name);
            ObjectPool.Instance.GetFromPool(bulletTurret, spawnPoint.position, spawnPoint.rotation);
            attackCooldown = attackInterval;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void TurretDestroyed()
    {
        gameObject.SetActive(false);
    }
}
