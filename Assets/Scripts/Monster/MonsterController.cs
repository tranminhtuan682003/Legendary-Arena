using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController, IHealth
{
    private IState currentState;
    private NavMeshAgent navMeshAgent;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    [Header("attack")]
    private bool canAtatck;
    private bool isFollowing;
    private Transform target;
    private Vector3 initPosition;
    private Quaternion initRotation;

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = new MonsterIdleState(this);
        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

        initPosition = transform.position;
        initRotation = transform.rotation;
    }

    void Update()
    {
        currentState.Execute();
    }

    #region State Management
    public void ChangeState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    #endregion

    #region MoveManager
    public void SetTarget(bool isFollowing, Transform target)
    {
        this.isFollowing = isFollowing;
        this.target = target;
    }

    public void FollowTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    public void ReturnHome()
    {
        navMeshAgent.SetDestination(initPosition);
        transform.rotation = initRotation;
    }

    public bool Attack()
    {
        if (isFollowing && target != null && canAtatck)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region HealthManager
    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        canAtatck = true;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Dead();
        }
    }

    public bool IsDead => CurrentHealth <= 0;

    public void Dead()
    {
        Debug.Log("Monster has died");
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }
    #endregion
}
