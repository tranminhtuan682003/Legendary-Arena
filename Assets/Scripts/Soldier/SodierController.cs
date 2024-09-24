using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : BaseController, IHealth
{
    private IState currentState;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    private NavMeshAgent navMeshAgent;
    public List<Transform> points;
    public Queue<Transform> targetPositions;
    private bool following;
    public bool isAttacking;
    public Transform target;

    protected override void Awake()
    {
        base.Awake();
        currentState = new SoldierIdleState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetPositions = new Queue<Transform>();
        AddTargetToQueue();
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
    private void AddTargetToQueue()
    {
        foreach (var item in points)
        {
            targetPositions.Enqueue(item);
        }
    }

    private void Move()
    {
        if (targetPositions.Count > 0 && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Transform nextTarget = targetPositions.Dequeue();
            navMeshAgent.SetDestination(nextTarget.position);
        }
    }

    public void SetTarget(bool following, Transform target)
    {
        this.following = following;
        this.target = target;
    }

    public void FollowEnemy()
    {
        if (following && target != null)
        {
            navMeshAgent.SetDestination(target.position);
            isAttacking = true;
        }
        else
        {
            Move();
            isAttacking = false;
        }
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
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Dead();
        }
    }

    public bool IsDead => CurrentHealth <= 0;

    public void Dead()
    {
        Debug.Log("Hero has died");
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }

    #endregion
}
