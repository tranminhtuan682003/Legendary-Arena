using System.Collections;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Unity.Mathematics;

public class KnightController : MonoBehaviour, ITeamMember
{
    #region Variables and Dependencies
    private IState currentState;
    private Team team;

    [Inject] private UIKnightManager uIKnightManager;
    [Inject] private SoundKnightManager soundKnightManager;
    private Animator animator;
    private Rigidbody2D rb;
    private GameObject effect;

    [Header("Move Settings")]
    private int currentHealth;
    private int maxHealth;
    private int maxMana;
    private int currentMana;
    private Canvas healthBar;
    private bool isTakeDamage;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded;
    private bool hasJumped;
    private Vector2 moveDirection;

    [Header("Attack Settings")]
    private Dictionary<TypeSkill, float> skillCooldowns = new Dictionary<TypeSkill, float>();
    public bool isExecuting = false;
    private GameObject shield;
    private Transform spawnPoint;

    [Header("Dead Settings")]
    public bool isDead;

    private Coroutine currentCoroutine; // Dùng để quản lý Coroutine hiện tại
    #endregion

    #region Initialization and Unity Lifecycle
    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        KnightEventManager.TriggerKnightEnable(transform);
        KnightEventManager.OnMoveActived += HandleMovement;
        KnightEventManager.OnSkillActived += HandleAttack;

        currentHealth = maxHealth;
        currentMana = maxMana;
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

    private void OnDisable()
    {
        KnightEventManager.OnMoveActived -= HandleMovement;
        KnightEventManager.OnSkillActived -= HandleAttack;
    }
    #endregion

    #region State Management
    public void ChangeState(IState newState)
    {
        if (currentState is KnightRecallState)
        {
            CancelRecall();
        }

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    #endregion

    #region Team and Health Management
    public Team GetTeam()
    {
        return team;
    }

    private void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        team = Team.Blue;
        maxHealth = 1000;
        maxMana = 100;
        healthBar = transform.Find("HealthBarManager").GetComponent<Canvas>();
        spawnPoint = transform.Find("SpawnPoint");
        shield = transform.Find("Shield").gameObject;
        effect = transform.Find("Recall").gameObject;

        shield.SetActive(false);
        effect.SetActive(false);

        foreach (TypeSkill skill in System.Enum.GetValues(typeof(TypeSkill)))
        {
            skillCooldowns[skill] = 0f;
        }
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    public float GetMana()
    {
        return (float)currentMana / maxMana;
    }

    #endregion

    #region Movement Handling
    public void HandleMovement(TypeMove typeMove)
    {
        // Hủy Recall trước khi di chuyển
        CancelRecall();

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
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
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
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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

    #region Attack and Skill Handling
    public void HandleAttack(TypeSkill typeSkill, float cooldown, float executionTime)
    {
        CancelRecall();
        if (!typeSkill.CanInterrupt() && isExecuting)
        {
            Debug.Log($"Cannot execute {typeSkill}: Another skill is in progress!");
            return;
        }

        if (Time.time < skillCooldowns[typeSkill])
        {
            Debug.Log($"{typeSkill} is on cooldown!");
            return;
        }

        ChangeState(new KnightAttackState(this, typeSkill, cooldown, executionTime));
    }



    public void SetSkillCooldown(TypeSkill typeSkill, float cooldown)
    {
        skillCooldowns[typeSkill] = Time.time + cooldown;
    }

    public void HandleAttackState(TypeSkill typeSkill, float cooldown, float executionTime)
    {
        // Đặt trạng thái "đang thực thi" nếu kỹ năng không phải loại có thể ngắt
        if (!typeSkill.CanInterrupt())
        {
            isExecuting = true;
        }

        switch (typeSkill)
        {
            case TypeSkill.Attack:
                StartCoroutine(PerformBasicAttack("Throw", executionTime));
                break;
            case TypeSkill.Skill1:
                StartCoroutine(Skill1("Throw", executionTime));
                break;
            case TypeSkill.Skill2:
                StartCoroutine(Skill2("Shield", executionTime));
                break;
            case TypeSkill.Skill3:
                StartCoroutine(Skill3("Skill1", executionTime));
                break;
            case TypeSkill.Recall:
                StartRecall("Fly", executionTime);
                break;
            case TypeSkill.Heal:
                StartCoroutine(Heal(executionTime));
                break;
            default:
                Debug.Log("Invalid skill type!");
                break;
        }
    }


    private void StartRecall(string nameAnimation, float executionTime)
    {
        CancelRecall(); // Hủy Recall nếu đang có
        currentCoroutine = StartCoroutine(RecallCoroutine(nameAnimation, executionTime));
    }

    private IEnumerator RecallCoroutine(string nameAnimation, float executionTime)
    {
        effect.SetActive(true);
        PlayAnimation(nameAnimation);
        var effectAnimator = effect.GetComponent<Animator>();
        effectAnimator.SetTrigger("Run");
        yield return new WaitForSeconds(executionTime); // Chờ Recall hoàn tất
        transform.position = new Vector3(-141f, 6, 0);
        effect.SetActive(false);
        currentCoroutine = null; // Dọn dẹp sau khi hoàn tất
        Debug.Log("Recall completed.");
    }

    private void CancelRecall()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        if (effect != null && effect.activeSelf)
        {
            effect.SetActive(false);
        }
    }

    private IEnumerator Heal(float cooldown)
    {
        int healPerSecond = 20;
        int manaPerSecond = 2;
        for (int i = 0; i < cooldown; i++)
        {
            KnightEventManager.InvokeUpdateHealthBar(this);
            currentHealth += healPerSecond;
            currentHealth = Mathf.Min(currentHealth, maxHealth);

            currentMana += manaPerSecond;
            currentMana = Mathf.Min(currentMana, maxMana);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator PerformBasicAttack(string nameAnimation, float executionTime)
    {
        PlayAnimation(nameAnimation);
        yield return new WaitForSeconds(executionTime / 2);
        uIKnightManager.GetBulletThrow(spawnPoint);
    }

    private IEnumerator Skill1(string nameAnimation, float executionTime)
    {
        TakeMana(10);
        PlayAnimation(nameAnimation);
        yield return new WaitForSeconds(executionTime / 2);
        uIKnightManager.GetSwordKnight(spawnPoint);
    }

    private IEnumerator Skill2(string nameAnimation, float executionTime)
    {
        soundKnightManager.PlayMusicSkill2();
        TakeMana(20);
        isTakeDamage = true;
        this.shield.SetActive(true);
        PlayAnimation(nameAnimation);
        var shieldAnimator = this.shield.GetComponent<Animator>();
        shieldAnimator.SetTrigger("Run");
        yield return new WaitForSeconds(executionTime);
        isTakeDamage = false;
        this.shield.SetActive(false);
    }

    private IEnumerator Skill3(string nameAnimation, float executionTime)
    {
        TakeMana(30);
        isTakeDamage = true;
        this.effect.SetActive(true);
        PlayAnimation(nameAnimation);
        var effectAnimator = this.effect.GetComponent<Animator>();
        effectAnimator.SetTrigger("Blood");
        yield return new WaitForSeconds(executionTime);
        isTakeDamage = false;
        this.effect.SetActive(false);
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

    public void TakeDamage(int amount)
    {
        if (isTakeDamage) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        KnightEventManager.InvokeUpdateHealthBar(this);
        if (currentHealth <= 0)
        {
            ChangeState(new KnightDeadState(this));
        }
    }


    public void TakeMana(int amount)
    {
        currentMana -= amount;
        KnightEventManager.InvokeUpdateManaBar(this);
    }

    public void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
    #endregion
}
