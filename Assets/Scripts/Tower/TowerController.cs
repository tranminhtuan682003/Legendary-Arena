using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour, IHealthTower
{
    #region Variables

    [Header("Tower Components")]
    public CapsuleCollider collisionCollider;
    public SphereCollider attackCollider;
    public SphereCollider effectCollider;
    private Rigidbody rb;

    [Header("Tower Stats")]
    private float maxHealth = 10000;
    private float attackInterval = 1.5f;
    private float currentHealth;
    private float lastAttackTime;
    private IState currentState;
    private bool isAttacking = false;
    private int enemiesInRangeEffect = 0;

    [Header("Attack & Visuals")]
    private LineRenderer attackLine;
    public Material colorLine;
    public Transform spawnPoint;
    private Transform currentTarget;
    private Queue<Transform> attackQueue = new Queue<Transform>();

    private AttackArea attackArea;

    [Header("Bullet Manager")]
    public GameObject bulletTowerPrefab;
    private int poolSize = 10;

    #endregion

    #region Initialization and Setup

    private void Start()
    {
        InitializeTower();
    }

    private void InitializeTower()
    {
        currentHealth = maxHealth;
        ChangeState(new TowerActiveState(this));
        SetupPool();
        SetupRigidBody();
        SetupLineRenderer();
    }

    private void SetupPool()
    {
        ObjectPool.Instance.CreatePool(bulletTowerPrefab, poolSize);
    }

    private void SetupRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void SetupLineRenderer()
    {
        attackLine = gameObject.AddComponent<LineRenderer>();
        attackLine.positionCount = 2;
        attackLine.startWidth = 0.1f;
        attackLine.endWidth = 0.1f;
        attackLine.startColor = Color.red;
        attackLine.endColor = Color.red;
        attackLine.material = colorLine;
        attackLine.enabled = false;

        attackArea = gameObject.GetComponentInChildren<AttackArea>();
        attackArea.SetRadius(attackCollider.radius);
    }

    #endregion

    #region Update Logic

    private void Update()
    {
        currentState?.Execute();
        CheckEffectRange();
        CheckAttackRange();

        if (currentTarget != null)
        {
            DrawLineToTarget();
        }
        else
        {
            DisableLine();
        }

        // Kiểm tra nếu đã đủ thời gian để thực hiện lần bắn tiếp theo
        if (isAttacking && Time.time - lastAttackTime >= attackInterval)
        {
            FireTarget();
            lastAttackTime = Time.time; // Cập nhật thời gian lần bắn cuối cùng
        }
    }

    #endregion

    #region IHealthTower Implementation

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public void TakeDamage(float damage)
    {
        if (currentState is TowerDestroyedState) return;

        currentHealth -= damage;
        Debug.Log($"Tower hit! Health left: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Đảm bảo máu không âm
            ChangeState(new TowerDestroyedState(this));
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Tower healed! Current Health: {currentHealth}");
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    #endregion

    #region Attack Logic

    private void CheckAttackRange()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackCollider.transform.position, attackCollider.radius * GetMaxScale(attackCollider));

        // Làm mới hàng đợi chỉ giữ lại kẻ địch trong phạm vi
        Queue<Transform> updatedQueue = new Queue<Transform>();

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                if (!attackQueue.Contains(enemy.transform))
                {
                    attackQueue.Enqueue(enemy.transform); // Thêm kẻ địch mới
                }
                updatedQueue.Enqueue(enemy.transform); // Giữ lại kẻ địch còn trong phạm vi
            }
        }

        attackQueue = updatedQueue; // Cập nhật hàng đợi mới

        // Nếu không có kẻ địch nào trong phạm vi
        if (attackQueue.Count == 0 && isAttacking)
        {
            currentTarget = null;
            DisableLine();
            ChangeState(new TowerActiveState(this));
            isAttacking = false;
        }
        else if (attackQueue.Count > 0 && currentTarget == null)
        {
            currentTarget = attackQueue.Peek(); // Lấy mục tiêu đầu tiên
            EnableLine();
            ChangeState(new TowerAttackState(this));
            isAttacking = true;

            // Chỉ bắn ngay nếu đủ thời gian từ lần bắn trước
            if (Time.time - lastAttackTime >= attackInterval)
            {
                FireTarget(); // Bắn mục tiêu mới
                lastAttackTime = Time.time; // Cập nhật thời gian bắt đầu tấn công
            }
        }
    }

    private void FireTarget()
    {
        if (currentTarget != null)
        {
            GameObject newProjectile = ObjectPool.Instance.GetFromPool(bulletTowerPrefab);
            newProjectile.transform.position = spawnPoint.position;
            newProjectile.GetComponent<BulletTowerController>().Fire(currentTarget, () =>
            {
                ObjectPool.Instance.ReturnToPool(bulletTowerPrefab, newProjectile);
            });
        }
    }

    private void DrawLineToTarget()
    {
        // Chỉ vẽ đường nối khi mục tiêu còn trong phạm vi
        if (currentTarget != null && Vector3.Distance(currentTarget.position, attackCollider.transform.position) <= attackCollider.radius * GetMaxScale(attackCollider))
        {
            attackLine.SetPosition(0, spawnPoint.position);
            attackLine.SetPosition(1, currentTarget.position);
        }
        else
        {
            DisableLine();
            currentTarget = null;
        }
    }

    private void EnableLine()
    {
        attackLine.enabled = true;
    }

    private void DisableLine()
    {
        attackLine.enabled = false;
    }

    #endregion

    #region Effect Logic

    private void CheckEffectRange()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(effectCollider.transform.position, effectCollider.radius * GetMaxScale(effectCollider));
        int currentEnemiesInRange = CountEnemies(hitEnemies);

        if (currentEnemiesInRange > 0 && enemiesInRangeEffect == 0)
            ActivateLightEffect();
        else if (currentEnemiesInRange == 0 && enemiesInRangeEffect > 0)
            DeactivateLightEffect();

        enemiesInRangeEffect = currentEnemiesInRange;
    }

    private void ActivateLightEffect()
    {
        attackArea.EnableLine();
    }

    private void DeactivateLightEffect()
    {
        attackArea.DisableLine();
    }

    #endregion

    #region Utilities

    private int CountEnemies(Collider[] hitEnemies)
    {
        int count = 0;
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
                count++;
        }
        return count;
    }

    private float GetMaxScale(SphereCollider collider)
    {
        return Mathf.Max(collider.transform.localScale.x, collider.transform.localScale.y, collider.transform.localScale.z);
    }

    #endregion

    #region State Management

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    #endregion
}
