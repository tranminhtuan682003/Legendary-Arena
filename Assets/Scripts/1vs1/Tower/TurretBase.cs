using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public abstract class TurretBase : MonoBehaviour, ITeamMember
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

    private Team team;
    public Team GetTeam() => team;

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

    protected virtual void Initialize(int maxHealth, float detectionRange, float attackRange, int attackDamage, Team team)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.team = team;
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
            if (item.name == name) return item.prefab;
        }

        Debug.LogWarning("Prefab not found!");
        return null;
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
        bool foundTarget = false;

        foreach (var hitCollider in hitColliders)
        {
            ITeamMember potentialTarget = hitCollider.GetComponent<ITeamMember>();
            if (potentialTarget != null && potentialTarget.GetTeam() != this.team)
            {
                target = hitCollider.transform;
                TurretEventManager.TriggerTargetDetected(target);
                UpdateStateBasedOnTarget();
                foundTarget = true;
                break;
            }
        }
        if (!foundTarget && !(currentState is TurretIdleState))
        {
            ChangeState(new TurretIdleState(this));
            target = null;
        }
    }

    private void UpdateStateBasedOnTarget()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange)
        {
            if (!(currentState is TurretAttackState))
                ChangeState(new TurretAttackState(this));
        }
        else if (!(currentState is TurretIdleState))
        {
            ChangeState(new TurretIdleState(this));
        }
    }

    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            Debug.Log("Soldier is attacking " + target.name);
            GameObject bulletObject = ObjectPool.Instance.GetFromPool(bulletTurret, spawnPoint.position, spawnPoint.rotation);
            if (bulletObject.TryGetComponent<BulletBase>(out var bullet))
            {
                bullet.Initialize(speedMove: 20f, damage: attackDamage, attackRange: attackRange);
            }

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

    public float GetCurrentHealth()
    {
        throw new System.NotImplementedException();
    }
}
