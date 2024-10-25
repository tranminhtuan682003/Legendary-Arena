using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SoldierBase : MonoBehaviour
{
    private IState currentState;
    private Rigidbody rb;
    private List<Vector3> roadMaps;
    private int currentRoadIndex;

    // Dictionary ánh xạ tag với các hàm xử lý mục tiêu
    private Dictionary<string, System.Action> targetHandlers;

    // Soldier Database
    [Header("Soldier Database")]
    private SupplyToTheTurret soldierDatabase;
    private const string SoldierDatabaseAddress = "Assets/Scripts/Soldier/SoldierDatabase.asset";
    private GameObject bulletSoldier;
    private Transform spawnPoint;

    // Các thuộc tính chiến đấu
    private int currentHealth;
    private int maxHealth;
    private float speedMove;
    private float detectionRange;
    private float attackRange;
    private int attackDamage;
    private float AttackInterval = 2f;

    private Transform target;
    private bool isChasingTarget = false; // Cờ kiểm tra xem có đang đuổi theo target không
    private float attackCooldown;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = new SoldierIdleState(this);
        currentState.Enter();
        InitItem();
    }

    protected virtual void Update()
    {
        currentState?.Execute();

        if (isChasingTarget && target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                ChangeState(new SoldierAttackState(this)); // Chuyển sang AttackState nếu target vào tầm bắn
            }
            else
            {
                ChaseTarget(); // Tiếp tục đuổi theo target nếu chưa vào tầm bắn
            }
        }
        else
        {
            HandleMove(); // Quay lại lộ trình khi không có target
        }

        DetectTarget(); // Luôn kiểm tra xem có target nào vào vùng phát hiện không
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual void Initialize(int maxHealth, float speedMove, float detectionRange, float attackRange, int attackDamage, List<Vector3> roadMaps, string tagEnemy, string tagSoldierEnemy, string tagTurretEnemy)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.speedMove = speedMove;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.roadMaps = roadMaps;
        currentRoadIndex = 0;

        // Khởi tạo Dictionary ánh xạ tag với phương thức xử lý tương ứng
        targetHandlers = new Dictionary<string, System.Action>
        {
            { tagEnemy, HandlePlayerTarget },
            { tagSoldierEnemy, HandleSoldierTarget },
            { tagTurretEnemy, HandleTurretTarget }
        };
    }

    private void InitItem()
    {
        spawnPoint = transform.Find("SpawnPoint");
        Addressables.LoadAssetAsync<SupplyToTheTurret>(SoldierDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                soldierDatabase = handle.Result;
                bulletSoldier = GetPrefabByName("BulletSoldier");
            }
        };
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in soldierDatabase.items)
        {
            if (item.name == name)
            {
                return item.prefab;
            }
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
            {
                currentRoadIndex++;
            }
        }
        else
        {
            Debug.Log("Reached the end of the path");
        }
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        // Đuổi theo target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speedMove * Time.deltaTime;

        if (!(currentState is SoldierMoveState))
        {
            ChangeState(new SoldierMoveState(this));
        }
    }

    private void DetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        bool foundTarget = false;

        foreach (var hitCollider in hitColliders)
        {
            if (targetHandlers.TryGetValue(hitCollider.tag, out System.Action handler))
            {
                target = hitCollider.transform;
                GamePlay1vs1Manager.Instance.targetOfSoldier = target;
                handler.Invoke();
                isChasingTarget = true;
                foundTarget = true;
                break;
            }
        }

        if (!foundTarget)
        {
            isChasingTarget = false; // Không có target trong vùng phát hiện, quay lại lộ trình
            target = null;
        }
    }

    // Các phương thức xử lý mục tiêu cụ thể
    private void HandlePlayerTarget()
    {
        UpdateStateBasedOnTarget();
    }

    private void HandleSoldierTarget()
    {
        UpdateStateBasedOnTarget();
    }

    private void HandleTurretTarget()
    {
        UpdateStateBasedOnTarget();
    }

    private void UpdateStateBasedOnTarget()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            ChangeState(new SoldierAttackState(this));
        }
        else
        {
            ChangeState(new SoldierMoveState(this));
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            ChangeState(new SoldierDeadState(this));
        }
    }

    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            Debug.Log("Soldier is attacking " + target.name);
            ObjectPool.Instance.GetFromPool(bulletSoldier, spawnPoint.position, spawnPoint.rotation);
            attackCooldown = AttackInterval;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
