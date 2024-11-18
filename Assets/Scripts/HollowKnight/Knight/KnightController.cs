using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class KnightController : MonoBehaviour
{
    private IState currentState;
    [Inject] private ButtonPlayKnightManager buttonKnightManager;
    private Animator animator;
    private Rigidbody2D rb;
    [Header("Move")]
    private Vector2 moveDirection = Vector2.zero;
    private float speed = 10f;
    private bool isGrounded = false;
    private bool hasJumped = false;

    [Header("Attack")]
    public TypeSkill skill;


    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        // Bắt đầu với trạng thái Idle
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
    }

    public void HandleMove()
    {
        moveDirection = buttonKnightManager.CurrentMoveDirection;

        if (moveDirection == Vector2.left || moveDirection == Vector2.right)
        {
            MoveHorizontally();
        }
        else if (moveDirection == Vector2.up)
        {
            HandleUpAction();
        }
        else if (moveDirection == Vector2.down)
        {
            HandleDownAction();
        }
        else
        {
            StopMovement();
        }
    }


    private void MoveHorizontally()
    {
        if (moveDirection != Vector2.zero)
        {
            FlipCharacter(moveDirection.x);
            Vector2 horizontalVelocity = new Vector2(moveDirection.x * speed, rb.velocity.y); // Giữ y (trọng lực)
            rb.velocity = horizontalVelocity;
        }
    }

    private void FlipCharacter(float direction)
    {
        if (direction != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -Mathf.Sign(direction);
            transform.localScale = scale;
        }
    }



    private void HandleUpAction()
    {
        if (isGrounded && !hasJumped)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            ChangeAnimation("Jump");
            hasJumped = true;
        }
    }


    private void HandleDownAction()
    {
        Debug.Log("Handling Down Action");
    }

    public bool IsMoving()
    {
        return buttonKnightManager.CurrentMoveDirection != Vector2.zero;
    }

    public void StopMovement()
    {
        moveDirection = Vector2.zero;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundKnight"))
        {
            ChangeAnimation("Idle");
            isGrounded = true;
            hasJumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GroundKnight"))
        {
            isGrounded = false;
        }
    }

    public void ChangeAnimation(string nameAnimation)
    {
        animator.ResetTrigger("Idle");
        // animator?.ResetTrigger("Run");
        animator.ResetTrigger("Jump");
        // animator?.ResetTrigger("Dead");
        animator.ResetTrigger("Throw");

        animator.SetTrigger(nameAnimation);
    }

    #region Handle Skills

    public void HandleAttack()
    {
        skill = buttonKnightManager.typeSkill;
        switch (skill)
        {
            case TypeSkill.Attack:
                StartCoroutine(PerformBasicAttack());
                break;

            case TypeSkill.Skill1:
                UseSkill1();
                break;

            case TypeSkill.Skill2:
                UseSkill2();
                break;

            case TypeSkill.Skill3:
                UseSkill3();
                break;

            case TypeSkill.Heal:
                HealKnight();
                break;

            case TypeSkill.Recall:
                RecallToBase();
                break;

            case TypeSkill.Supplymentary:
                ApplySupplementaryEffect();
                break;

            default:
                break;
        }
    }

    private IEnumerator PerformBasicAttack()
    {
        ChangeAnimation("Throw");
        buttonKnightManager.OnButtonAttackReleased();
        yield return new WaitForSeconds(5 / 6f);
        ChangeState(new KnightIdleState(this));
    }


    private void UseSkill1()
    {
        Debug.Log("Using Skill 1");
    }

    private void UseSkill2()
    {
        Debug.Log("Using Skill 2");
    }

    private void UseSkill3()
    {
        Debug.Log("Using Skill 3");
    }

    private void HealKnight()
    {
        Debug.Log("Healing Knight");
    }

    private void RecallToBase()
    {
        Debug.Log("Recalling to base");
    }

    private void ApplySupplementaryEffect()
    {
        Debug.Log("Applying supplementary effect");
    }

    public bool IsAttacking()
    {
        return buttonKnightManager.typeSkill != TypeSkill.None;
    }

    #endregion

}
