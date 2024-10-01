using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IhealthPlayer
{
    public Heros currentHero;
    public SphereCollider attackCollider;
    private IState currentState;
    private Rigidbody rb;
    private Animator animator;

    [HideInInspector]
    public Vector2 moveDirection;
    private Vector3 movementVector;

    [Header("IHealth")]
    private float maxHealth = 5000f;
    private float currentHealth;
    public bool isDead;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    #region IHealthPlayer Implementation
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(0, value);
    }

    public bool IsDead => isDead;
    #endregion

    void Awake()
    {
        currentHero = new Marksman();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        isDead = false;
    }

    void Start()
    {
        ChangeState(new PlayerIdleState(this));
        SkillUIManager skillUIManager = FindObjectOfType<SkillUIManager>();
        skillUIManager.SetupSkillButtons();
    }


    void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeAnimator(string nameAnimation)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dead");

        animator.SetTrigger(nameAnimation);
    }

    #region Attack
    public void ActivateAbility(int abilityIndex)
    {
        if (abilityIndex < currentHero.abilities.Count)
        {
            currentHero.abilities[abilityIndex].Activate();
        }
    }

    #endregion

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>().normalized;
        movementVector = new Vector3(moveDirection.x, 0, moveDirection.y);
    }

    public void Move()
    {
        if (movementVector != Vector3.zero && !isDead)
        {
            Vector3 move = movementVector * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + move);

            // Quay nhân vật theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Healed: " + amount);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
        ChangeAnimator("Dead");
        Debug.Log("Player is dead.");
    }
}
