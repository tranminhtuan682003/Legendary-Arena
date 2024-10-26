using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class HeroBase : MonoBehaviour, ITeamMember
{
    #region Components
    private Rigidbody rb;
    private Animator animator;
    protected IState currentState;
    private GameObject bulletHero;
    private string nameBulletHero;
    private Transform spawnPoint;
    #endregion

    #region Team and Database
    [SerializeField] private Team team;
    private HeroDatabasee heroDatabase;
    private string HeroDatabaseAddress;

    public Team GetTeam() => team;
    #endregion

    #region Health
    [Header("Health")]
    protected int currentHealth;
    protected int maxHealth;
    public bool IsDead => currentHealth <= 0;
    #endregion

    #region Movement
    [Header("Movement")]
    protected float speedMove;
    public Vector2 moveDirection;
    public Vector3 movementVector;
    public bool isMoving;
    #endregion

    #region Attack
    [Header("Attack")]
    public float detectionRange;
    public float attackRange;
    public int attackDamage;
    private float attackInterval = 2f;
    private float attackCooldown = 0f;
    #endregion

    #region Targeting
    private Transform target;
    #endregion

    protected virtual void Start()
    {
        HeroEventManager.TriggerHeroCreated(this);
        currentState = new HerroIdleState(this);
        currentState.Enter();
        InitComponents();
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

    protected virtual void Initialize(int maxHealth, float speedMove, float detectionRange, float attackRange, int attackDamage, Team team, string HeroDatabaseAddress, string nameBulletHero)
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
                foundTarget = true;
                break;
            }
        }

        if (!foundTarget)
        {
            target = null;
        }
    }

    private bool IsValidTarget(Collider hitCollider)
    {
        ITeamMember potentialTarget = hitCollider.GetComponent<ITeamMember>();
        return potentialTarget != null && potentialTarget.GetTeam() != this.team;
    }

    public void HandleAttack()
    {
        if (target != null && attackCooldown <= 0f)
        {
            Debug.Log("Soldier is attacking " + target.name);
            GameObject bulletObject = ObjectPool.Instance.GetFromPool(bulletHero, spawnPoint.position, spawnPoint.rotation);
            if (bulletObject.TryGetComponent<BulletBase>(out var bullet))
            {
                bullet.Initialize(speedMove: 20f, target: target, damage: attackDamage);
            }

            attackCooldown = attackInterval;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
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
        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    public void Move()
    {
        if (movementVector != Vector3.zero && !IsDead)
        {
            isMoving = true;
            Vector3 move = movementVector * speedMove * Time.deltaTime;
            rb.MovePosition(transform.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
        }
        else
        {
            isMoving = false;
        }
    }
    #endregion
}
