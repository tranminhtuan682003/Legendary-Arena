using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SoldierBase : MonoBehaviour, ITeamMember
{
    private IState currentState;
    private Rigidbody rb;
    private Team team;
    public Team GetTeam() => team;

    private List<Vector3> roadMaps;
    private int currentRoadIndex;
    private string nameBullet;

    // Soldier Database
    [Header("Soldier Database")]
    private SupplyToTheTurret soldierDatabase;
    private const string SoldierDatabaseAddress = "Assets/Scripts/Soldier/SoldierDatabase.asset";
    private GameObject bulletSoldier;
    private Transform spawnPoint;

    // Combat Attributes
    private int currentHealth;
    private int maxHealth;
    private float speedMove;
    private float detectionRange;
    private float attackRange;
    private int attackDamage;
    private float attackInterval = 2f;

    private Transform target;
    private bool isChasingTarget = false;
    private float attackCooldown;

    protected virtual void Start()
    {
        currentState = new SoldierIdleState(this);
        currentState.Enter();
        InitItem();
    }

    protected virtual void Update()
    {
        currentState?.Execute();
        ProcessTargeting();
        DetectTarget();
    }

    private void ProcessTargeting()
    {
        if (isChasingTarget && target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
                ChangeState(new SoldierAttackState(this));
            else
                ChaseTarget();
        }
        else
        {
            HandleMove();
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual void Initialize(int maxHealth, float speedMove, float detectionRange, float attackRange, int attackDamage, List<Vector3> roadMaps, Team team, string nameBullet)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.speedMove = speedMove;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.roadMaps = roadMaps;
        this.team = team;
        this.nameBullet = nameBullet;
        this.currentRoadIndex = 0;
    }

    private void InitItem()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.Find("SpawnPoint");
        Addressables.LoadAssetAsync<SupplyToTheTurret>(SoldierDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                soldierDatabase = handle.Result;
                bulletSoldier = GetPrefabByName(nameBullet);
            }
        };
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in soldierDatabase.items)
        {
            if (item.name == name) return item.prefab;
        }
        Debug.LogWarning("Prefab not found!");
        return null;
    }

    public void HandleMove()
    {
        if (currentRoadIndex < roadMaps.Count)
        {
            Vector3 targetPosition = roadMaps[currentRoadIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 1f)
                currentRoadIndex++;
        }
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speedMove * Time.deltaTime;

        if (!(currentState is SoldierMoveState))
            ChangeState(new SoldierMoveState(this));
    }

    private void DetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        bool foundTarget = false;

        foreach (var hitCollider in hitColliders)
        {
            ITeamMember potentialTarget = hitCollider.GetComponent<ITeamMember>();
            if (potentialTarget != null && potentialTarget.GetTeam() != this.team)
            {
                target = hitCollider.transform;
                SoldierEventManager.TriggerTargetDetected(target);
                isChasingTarget = true;
                foundTarget = true;
                break;
            }
        }
        if (!foundTarget)
        {
            isChasingTarget = false;
            target = null;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            ChangeState(new SoldierDeadState(this));
    }

    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            GameObject bulletObject = ObjectPool.Instance.GetFromPool(bulletSoldier, spawnPoint.position, spawnPoint.rotation);
            if (bulletObject.TryGetComponent<BulletBase>(out var bullet))
            {
                bullet.Initialize(speedMove: 20f, damage: attackDamage, attackRange: attackRange);
            }
            RotateToTarget();
            attackCooldown = attackInterval;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    private void RotateToTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 180f);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    public float GetCurrentHealth()
    {
        throw new System.NotImplementedException();
    }
}
