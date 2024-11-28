using UnityEngine;
using Zenject;
using System;
public class TowerKnightBase : MonoBehaviour, ITeamMember
{
    protected Team team;
    protected Team teamEnemy;
    protected IState currentState;
    protected Transform spawnPoint;

    protected int currentHealth;
    protected int maxHealth;

    protected GameObject currentEnemy;
    protected float attackRange;
    protected HealthBarTeamRedController healthBarController;

    [Inject] protected UIKnightManager uIKnightManager;

    private void Awake()
    {
        InitLize();

    }
    private void OnEnable()
    {
        InitLize();
    }

    private void Start()
    {
        ChangeState(new TowerKnightIdleState(this));
    }

    private void Update()
    {
        currentState?.Execute();
        DetectEnemyAndHandle();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public Team GetTeam()
    {
        return team;
    }

    private void InitLize()
    {
        team = Team.Red;
        spawnPoint = transform.Find("SpawnPoint");
        maxHealth = 2000;
        currentHealth = maxHealth;
        healthBarController = GetComponentInChildren<HealthBarTeamRedController>();
        healthBarController.SetParrent(this);
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    #region Handle Attack
    private void DetectEnemyAndHandle()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        bool foundEnemy = false;
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<ITeamMember>();
            if (enemy != null && enemy.GetTeam() == teamEnemy)
            {
                foundEnemy = true;
                GameObject enemyGameObject = collider.gameObject;
                if (currentEnemy == null || Vector3.Distance(transform.position, currentEnemy.transform.position) > attackRange)
                {
                    currentEnemy = enemyGameObject;
                    return;
                }
            }
        }

        if (!foundEnemy)
        {
            currentEnemy = null;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        float normalizedHealth = GetCurrentHealth();

        if (healthBarController != null)
        {
            healthBarController.UpdateHealth(normalizedHealth);
        }
        if (currentHealth <= 0)
        {
            ChangeState(new TowerKnightDeadState(this));
        }
    }

    public void HandleDetectEnemy(GameObject enemy, Team team)
    {

    }

    public void FireBullet(GameObject enemy)
    {

    }

    #endregion

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
