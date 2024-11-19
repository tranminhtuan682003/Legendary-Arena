using System.Collections;
using UnityEngine;
using Zenject;

public class KnightController : MonoBehaviour
{
    private IState currentState;
    [Inject] private ButtonControlManager buttonManager;
    [Inject] private UIKnightManager uIKnightManager;
    [Inject] private KnightPlayScreenManager knightPlayScreenManager;
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
    public TypeMove currentMove;

    private Vector2 moveDirection;

    [Header("Attack Settings")]
    public TypeSkill currentSkill;
    [SerializeField] private float cooldown;
    private Transform spawnPoint;
    [Header("Dead Settings")]
    public bool isDead;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    private void Start()
    {
        ChangeState(new KnightIdleState(this));
    }

    private void Update()
    {
        currentState?.Execute(); // Gọi logic state hiện tại
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit(); // Thoát state hiện tại
        currentState = newState; // Gán state mới
        currentState.Enter(); // Khởi động state mới
    }

    private void Initialize()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 1000;
        spawnPoint = transform.Find("SpawnPoint");
        if (knightPlayScreenManager != null)
        {
            Debug.Log("khong Null");
            knightPlayScreenManager.SetMaxHealthBar(maxHealth);
        }
        else
        {
            Debug.Log("Null");
        }
    }

    #region Handle Move Logic
    public void HandleMovement()
    {
        // Gọi các hàm xử lý dựa trên hướng hiện tại
        switch (currentMove)
        {
            case TypeMove.Left: MoveHorizontally(-1); break;
            case TypeMove.Right: MoveHorizontally(1); break;
            case TypeMove.Up: Jump(); break;
            case TypeMove.Down: Crouch(); break;
            case TypeMove.None: StopMovement(); break;
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
        if (isGrounded)
        {
            PlayAnimation("Idle");
        }
    }

    private void FlipCharacter(float direction)
    {
        if (direction == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(-direction);
        transform.localScale = scale;

        // Cập nhật spawnPoint
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
    private bool isOnCooldown = false;

    public void HandleAttack()
    {
        if (isOnCooldown) return; // Nếu đang cooldown, bỏ qua
        isOnCooldown = true;

        currentSkill = buttonManager.currentSkill;

        switch (currentSkill)
        {
            case TypeSkill.Attack:
                StartCoroutine(PerformBasicAttack());
                break;
            case TypeSkill.Skill1:
                UseSkill("Skill1");
                break;
            case TypeSkill.Skill2:
                UseSkill("Skill2");
                break;
            case TypeSkill.Skill3:
                UseSkill("Skill3");
                break;
            default:
                break;
        }
    }

    private IEnumerator PerformBasicAttack()
    {
        PlayAnimation("Throw");
        yield return new WaitForSeconds(GetCooldown() / 2);
        uIKnightManager.GetBulletThrow(spawnPoint);

        yield return new WaitForSeconds(GetCooldown() / 2);
        isOnCooldown = false; // Reset cooldown sau khi hoàn thành
    }


    private void UseSkill(string skillAnimation)
    {
        // PlayAnimation(skillAnimation);
        Debug.Log("Used Skill: " + skillAnimation);
    }
    #endregion

    #region Utility Methods
    public void UpdateCurrentMove() => currentMove = buttonManager.currentMove;
    public void UpdateCurrentSkill() => currentSkill = buttonManager.currentSkill;
    public void SetCooldown() => cooldown = buttonManager.currentCooldown;
    public float GetCooldown() => cooldown;

    public void PlayAnimation(string animationName)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Throw");
        animator.SetTrigger(animationName);
    }
    #endregion

    #region Utility Methods
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
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

}
