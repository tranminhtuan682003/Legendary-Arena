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
    public Dictionary<string, ParticleSystem> particleEffects;

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
        ObjectPool.Instance.CreatePool(heroEffects.bulletPrefab);
        InitializeEffects();
    }

    void Update()
    {
        currentState?.Execute();
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
        currentHealth = maxHealth;
        isDead = false;
        ChangeState(new PlayerIdleState(this));
        SetupHealthBar();

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
            particleEffects = new Dictionary<string, ParticleSystem>
            {
                { "shootEffect", Instantiate(heroEffects.shootEffect) },
                { "returnHomeEffect", Instantiate(heroEffects.returnHomeEffect) },
                { "skill1", Instantiate(heroEffects.Skill1) },
                { "skill2", Instantiate(heroEffects.Skill2) },
                { "skill3", Instantiate(heroEffects.Skill3) }
            };
            foreach (var effect in particleEffects.Values)
            {
                effect.Stop();
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
    public void ActivateAbility(int abilityIndex)
    {
        if (abilityIndex < currentHero.abilities.Count)
        {
            currentHero.abilities[abilityIndex].Activate();
        }
    }

    public void ActivateEffect(string effectName, Transform position, float duration)
    {
        if (particleEffects.ContainsKey(effectName))
        {
            ParticleSystem effect = particleEffects[effectName];
            effect.transform.position = position.position;
            effect.Play();
            StartCoroutine(DeactivateEffect(effect, duration));
        }
    }

    IEnumerator DeactivateEffect(ParticleSystem effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.Stop();
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
        Debug.Log("Player is dead.");
    }
    #endregion
}
