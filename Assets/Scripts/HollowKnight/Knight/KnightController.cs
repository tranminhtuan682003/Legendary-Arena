using System.Collections;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class KnightController : MonoBehaviour
{
    private IState currentState;
    [Inject] private UIKnightManager uIKnightManager;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Move Settings")]
    private int currentHealth;
    private int maxHealth;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded;
    private bool hasJumped;
    private Vector2 moveDirection;

    [Header("Attack Settings")]
    private Dictionary<TypeSkill, float> skillCooldowns = new Dictionary<TypeSkill, float>();
    public bool isExecuting = false;
    private Transform spawnPoint;
    [Header("Dead Settings")]
    public bool isDead;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        KnightEventManager.TriggerKnightEnable(transform);
        KnightEventManager.InvokeSetHealthBar(this);
        KnightEventManager.OnMoveActived += HandleMovement;
        KnightEventManager.OnSkillActived += HandleAttack;

        currentHealth = maxHealth;
        isDead = false;
    }

    private void Start()
    {
        ChangeState(new KnightIdleState(this));
    }

    private void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 1000;
        spawnPoint = transform.Find("SpawnPoint");
        foreach (TypeSkill skill in System.Enum.GetValues(typeof(TypeSkill)))
        {
            skillCooldowns[skill] = 0f;
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetHealth()
    {
        return currentHealth;
    }

    #region Handle Move Logic
    public void HandleMovement(TypeMove typeMove)
    {
        if (typeMove != TypeMove.None)
        {
            ChangeState(new KnightMoveState(this, typeMove));
        }
        else
        {
            ChangeState(new KnightIdleState(this));
            StopMovement();
        }
    }
    public void HandleMoveState(TypeMove typeMove)
    {
        switch (typeMove)
        {
            case TypeMove.Left: MoveHorizontally(-1); break;
            case TypeMove.Right: MoveHorizontally(1); break;
            case TypeMove.Up: Jump(); break;
            case TypeMove.Down: Crouch(); break;
        }
    }

    private void MoveHorizontally(float direction)
    {
        moveDirection = new Vector2(direction, 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        FlipCharacter(direction);
    }

    private void Jump()
    {
        if (isGrounded && !hasJumped)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            PlayAnimation("Jump");
            hasJumped = true;
        }
    }

    private void Crouch()
    {
        if (isGrounded)
        {
            Debug.Log("Knight is crouching");
        }
    }

    public void StopMovement()
    {
        moveDirection = Vector2.zero;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void FlipCharacter(float direction)
    {
        if (direction == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(-direction);
        transform.localScale = scale;
        UpdateSpawnPointDirection(direction);
    }

    private void UpdateSpawnPointDirection(float direction)
    {
        if (spawnPoint != null)
        {
            spawnPoint.localRotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundKnight"))
        {
            isGrounded = true;
            hasJumped = false;
            PlayAnimation("Idle");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundKnight"))
        {
            isGrounded = false;
        }
    }
    #endregion

    #region Handle Attack Logic

    public void HandleAttack(TypeSkill typeSkill, float cooldown, float executionTime)
    {
        if (isExecuting)
        {
            Debug.LogWarning("khong the su dung ");
            return;
        }
        if (Time.time < skillCooldowns[typeSkill])
        {
            Debug.LogWarning($"Skill {typeSkill} is on cooldown. Remaining: {skillCooldowns[typeSkill] - Time.time:0.00}s");
            return;
        }
        ChangeState(new KnightAttackState(this, typeSkill, cooldown, executionTime));
    }

    public void SetSkillCooldown(TypeSkill typeSkill, float cooldown)
    {
        skillCooldowns[typeSkill] = Time.time + cooldown;
    }

    public void HandleAttackState(TypeSkill typeSkill, float cooldown)
    {
        isExecuting = true;
        switch (typeSkill)
        {
            case TypeSkill.Attack:
                StartCoroutine(PerformBasicAttack(cooldown));
                break;
            case TypeSkill.Skill1:
                break;
            case TypeSkill.Skill2:
                break;
            case TypeSkill.Skill3:
                UseSkill("Skill1");
                break;
            default:
                break;
        }
    }

    private IEnumerator PerformBasicAttack(float cooldown)
    {
        PlayAnimation("Throw");
        yield return new WaitForSeconds(cooldown / 2);
        uIKnightManager.GetBulletThrow(spawnPoint);
    }

    private void UseSkill(string skillAnimation)
    {
        PlayAnimation(skillAnimation);
    }
    #endregion

    #region Utility Methods
    public void PlayAnimation(string animationName)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Throw");
        animator.ResetTrigger("Skill1");
        animator.SetTrigger(animationName);
    }
    #endregion

    #region Utility Methods
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        KnightEventManager.InvokeUpdateHealthBar(this);
        if (currentHealth <= 0)
        {
            ChangeState(new KnightDeadState(this));
        }
    }

    public void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
    #endregion

    private void OnDisable()
    {
        KnightEventManager.OnMoveActived -= HandleMovement;
        KnightEventManager.OnSkillActived -= HandleAttack;
    }

}
