using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public abstract class HeroBase : MonoBehaviour, ITeamMember
{
    #region Components
    private Rigidbody rb;
    private Animator animator;
    private IState currentState;
    public bool isActive;
    private GameObject bulletHero;
    private string nameBulletHero;
    public Transform spawnPoint;
    #endregion

    #region Team and Database
    private Team team;
    private HeroDatabasee heroDatabase;
    private string HeroDatabaseAddress;

    public Team GetTeam() => team;
    #endregion

    #region Health
    [Header("Health")]
    public int currentHealth;
    public int maxHealth;
    public bool IsDead => currentHealth <= 0;
    #endregion

    #region Movement
    [Header("Movement")]
    private float speedMove;
    public Vector2 moveDirection;
    public Vector3 movementVector;
    public bool isMoving;
    #endregion

    #region Attack
    [Header("Attack")]
    public bool foundTarget;
    public bool isAttacking;
    public float detectionRange;
    public float attackRange;
    public int attackDamage;
    public float attackInterval;
    public float attackCooldown = 0f;
    #endregion

    #region Targeting
    public Transform target;
    #endregion

    protected virtual void Start()
    {
        HeroEventManager.TriggerHeroCreated(this);
        InitComponents();
        currentState = new HeroIdleState(this);
        currentState.Enter();
    }

    protected virtual void Update()
    {
        currentState?.Execute();
        DetectTarget();
        attackCooldown -= Time.deltaTime;
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeAnimation(string nameAnimation)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dead");
        animator.SetTrigger(nameAnimation);
    }

    public float TimeRunAnimation(string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return -1f;
    }

    protected virtual void Initialize(int maxHealth, float speedMove, float detectionRange, float attackRange, int attackDamage, Team team, string HeroDatabaseAddress, string nameBulletHero, float attackInterval)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.speedMove = speedMove;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
        this.team = team;
        this.HeroDatabaseAddress = HeroDatabaseAddress;
        this.nameBulletHero = nameBulletHero;
        this.attackInterval = attackInterval;
        LoadHeroDatabase();
    }

    private void InitComponents()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.Find("SpawnPoint");
    }

    private void LoadHeroDatabase()
    {
        Addressables.LoadAssetAsync<HeroDatabasee>(HeroDatabaseAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                heroDatabase = handle.Result;
                bulletHero = GetPrefabByName(nameBulletHero);
            }
            else
            {
                Debug.LogError("Failed to load Hero database.");
            }
        };
    }

    private GameObject GetPrefabByName(string name)
    {
        foreach (var item in heroDatabase.data)
        {
            if (item.name == name) return item.gameObject;
        }
        Debug.LogWarning("Prefab not found!");
        return null;
    }

    private void DetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        bool foundTarget = false;

        foreach (var hitCollider in hitColliders)
        {
            if (IsValidTarget(hitCollider))
            {
                target = hitCollider.transform;
                HeroEventManager.TriggerTargetDetected(target);
                foundTarget = true;
                break;
            }
        }

        if (foundTarget && target != null && target.gameObject.activeInHierarchy)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            isAttacking = distanceToTarget <= attackRange;
        }
        else
        {
            target = null;
            isAttacking = false;
        }
    }


    private bool IsValidTarget(Collider hitCollider)
    {
        ITeamMember potentialTarget = hitCollider.GetComponent<ITeamMember>();
        return potentialTarget != null && potentialTarget.GetTeam() != this.team;
    }


    public void FollowTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.position += directionToTarget * speedMove * Time.deltaTime;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void HandleAttack()
    {
        if (target != null)
        {
            RotateToTarget();
        }
        GameObject bulletObject = ObjectPool.Instance.GetFromPool(bulletHero, spawnPoint.position, spawnPoint.rotation);
        if (bulletObject.TryGetComponent<BulletBase>(out var bullet))
        {
            bullet.Initialize(speedMove: 20f, damage: attackDamage, attackRange: attackRange);
        }
    }

    private void RotateToTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 180f);
    }

    #region Health Methods
    public void Heal(int amount)
    {
        if (IsDead) return;
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("Hero is Dead!");
    }
    #endregion

    #region Movement Methods
    protected void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;

        // Lật hướng di chuyển nếu đội là "Red"
        if (GetTeam() == Team.Red)
        {
            moveDirection = -moveDirection;
        }

        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    public void HandleMove()
    {
        isMoving = true;
        Vector3 move = movementVector * speedMove * Time.deltaTime;
        rb.MovePosition(transform.position + move);

        // Xoay đối tượng về hướng di chuyển
        if (movementVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
        }
    }

    #endregion
}
