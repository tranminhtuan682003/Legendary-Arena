using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class PlayerController : MonoBehaviour, IhealthPlayer
{
    [Header("Start & Hero Setup")]
    public Transform startPosition;
    public Heros currentHero;
    private IState currentState;
    private Rigidbody rb;
    private Animator animator;

    [Header("Effect")]
    public HeroEffects heroEffects;
    public Dictionary<string, ParticleSystem> skillEffect;
    public SupplementaryTable supplementaryTable;
    [HideInInspector] public GameObject supplymentary;
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [HideInInspector] public Vector2 moveDirection;
    private Vector3 movementVector;
    [HideInInspector] public bool isMoving;

    [Header("Health Settings")]
    private float maxHealth = 5000f;
    private float currentHealth;
    public Slider healthBar;
    [HideInInspector] public bool isDead;

    [Header("Attack Settings")]
    private AttackArea attackArea;
    public Transform SpawnPoint;
    public List<GameObject> enemy;
    public List<GameObject> tower;
    [HideInInspector] public bool isAttacking;

    #region IHealthPlayer Implementation
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            healthBar.value = currentHealth;
            if (currentHealth <= 0 && !isDead)
            {
                ChangeState(new PlayerDeadState(this));
            }
        }
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(0, value);
    }

    public bool IsDead => isDead;
    #endregion

    #region Unity Methods
    void Awake()
    {
        InitializeComponents();
    }

    void Start()
    {
        InitializePlayer();
        InitializeEffects();
    }

    void Update()
    {
        currentState?.Execute();
        DetectEnemiesInRange();
    }
    #endregion

    #region Initialization Methods
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        currentHero = new Marksman(this);
    }

    private void InitializePlayer()
    {
        InitSupplementary("Explosive");
        ObjectPool.Instance.CreatePool(heroEffects.bulletPrefab);
        currentHealth = maxHealth;
        isDead = false;
        ChangeState(new PlayerIdleState(this));
        SetupHealthBar();
        heroEffects.timeAnimationAttack = GetAnimationClipLength("Attack", animator);
        heroEffects.timeShoot = heroEffects.timeAnimationAttack;

        SkillUIManager skillUIManager = FindObjectOfType<SkillUIManager>();
        skillUIManager.SetupSkillButtons();

        attackArea = gameObject.GetComponentInChildren<AttackArea>();
        heroEffects.rangeAttack = 2.5f;
    }

    private void SetupHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    private void InitializeEffects()
    {
        if (heroEffects != null)
        {
            skillEffect = new Dictionary<string, ParticleSystem>
            {
                { "shootEffect", Instantiate(heroEffects.shootEffect) },
                { "returnHomeEffect", Instantiate(heroEffects.returnHomeEffect) },
                { "skill1", Instantiate(heroEffects.Skill1) },
                { "skill2", Instantiate(heroEffects.Skill2) },
                { "skill3", Instantiate(heroEffects.Skill3) }
            };
            foreach (var effect in skillEffect.Values)
            {
                effect.Stop();
            }
        }
    }

    private void InitSupplementary(string name)
    {
        foreach (var item in supplementaryTable.supplementarys)
        {
            if (item.name == name)
            {
                supplymentary = Instantiate(item, transform);
                supplymentary.SetActive(false);
            }
        }
    }
    #endregion

    #region State & Animation Management
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeAnimator(string animationName)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dead");
        animator.SetTrigger(animationName);
    }

    private float GetAnimationClipLength(string animationName, Animator animator)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return -1f;
    }

    private void SetAnimationSpeed(string animationName, float speed)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName))
        {
            animator.speed = speed;
        }
    }
    #endregion

    #region Attack & Effects

    public void SetMaxRange(float maxRange)
    {
        heroEffects.rangeAttack = maxRange;
        attackArea.SetRadius(heroEffects.rangeAttack * 10);
    }

    public void ActivateLightEffect()
    {
        attackArea.EnableLine();
    }

    public void DeactivateLightEffect()
    {
        attackArea.DisableLine();
    }

    public void DetectEnemiesInRange()
    {
        Vector3 heroPosition = transform.position;
        float detectionRadius = (heroEffects.rangeAttack + 0.5f) * 2;
        Collider[] hitColliders = Physics.OverlapSphere(heroPosition, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Tower"))
            {
                Debug.Log("Tower detected: " + hitCollider.gameObject.name);
                tower.Add(hitCollider.gameObject);
            }
        }
    }

    public void ActivateAbility(int abilityIndex)
    {
        if (abilityIndex < currentHero.abilities.Count)
        {
            currentHero.abilities[abilityIndex].Activate();
        }
    }

    public void Attack()
    {
        speedShoot();
        ChangeAnimator("Attack");
        ActivateEffect("shootEffect", SpawnPoint, 0.5f);
        GameObject bullet = ObjectPool.Instance.GetFromPool(heroEffects.bulletPrefab, SpawnPoint.position, SpawnPoint.rotation);
        bullet.GetComponent<BulletTelAnas>().SetMaxRange(heroEffects.rangeAttack);
        if (heroEffects.rangeAttack == 2.5f)
        {
            StartCoroutine(ShowAttackArea());
        }
    }

    private IEnumerator ShowAttackArea()
    {
        ActivateLightEffect();
        yield return new WaitForSeconds(0.5f);
        DeactivateLightEffect();
    }

    public void ActivateEffect(string effectName, Transform position, float duration)
    {
        if (skillEffect.ContainsKey(effectName))
        {
            ParticleSystem effect = skillEffect[effectName];
            effect.transform.position = position.position;
            effect.Play();
            StartCoroutine(DeactivateEffect(effect, duration));
        }
    }

    private IEnumerator DeactivateEffect(ParticleSystem effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.Stop();
    }

    public void AdjustSpeedShoot(float timeShoot)
    {
        heroEffects.timeShoot = timeShoot;
    }

    public void speedShoot()
    {
        StartCoroutine(ChangeSpeedShoot());
    }

    private IEnumerator ChangeSpeedShoot()
    {
        isAttacking = true;
        yield return new WaitForSeconds(heroEffects.timeShoot);
        isAttacking = false;
    }
    #endregion

    #region Movement Methods
    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    public void Move()
    {
        if (movementVector != Vector3.zero && !isDead)
        {
            isMoving = true;
            Vector3 move = movementVector * moveSpeed * Time.deltaTime;
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

    #region Health Management
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        Debug.Log("Healed: " + amount);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log("Took damage: " + amount);
    }

    public void Dead()
    {
        isDead = true;
        ChangeAnimator("Dead");
    }
    #endregion
}
