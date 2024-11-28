using UnityEngine;
using Zenject;

public abstract class SoldierKnightBase : MonoBehaviour, ITeamMember
{
    private IState currentState;
    [Inject] protected UIKnightManager uIKnightManager;
    protected Team team;
    protected Team teamEnemy;
    protected int maxHealth;
    protected int currentHealth;
    protected HealthBarTeamRedController healthBarController;
    protected GameObject currentEnemy;
    protected float attackRange;
    protected float speedMove;
    protected Vector3 direction;
    protected Transform spawnPoint;

    private void OnEnable()
    {
        InitLize();
    }

    private void Start()
    {
        ChangeState(new SoldierKnightMoveState(this));
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

    protected virtual void InitLize() { }

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
                    ChangeState(new SoldierKnightChaseEnemyState(this, currentEnemy));
                    return;
                }
            }
        }

        if (!foundEnemy)
        {
            currentEnemy = null;
            ChangeState(new SoldierKnightMoveState(this));
        }
    }

    public void ChaseEnemy(GameObject currentEnemy)
    {
        if (currentEnemy == null) return;
        Vector3 enemyPosition = currentEnemy.transform.position;
        Vector3 direction = (enemyPosition - transform.position).normalized;
        transform.position += direction * speedMove * Time.deltaTime;
        if (Vector3.Distance(transform.position, enemyPosition) <= 5f)
        {
            ChangeState(new SoldierKnightAttackState(this, currentEnemy));
        }
    }

    public virtual void FireBullet(GameObject enemy) { }

    public void Move()
    {
        transform.position += direction * Time.deltaTime * speedMove;
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            ChangeState(new SoldierKnightDeadState(this));
        }
    }

    public void UpdateHealthBar()
    {
        float normalizedHealth = GetCurrentHealth();
        if (healthBarController != null)
        {
            healthBarController.UpdateHealth(normalizedHealth);
        }
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    public Team GetTeam()
    {
        return team;
    }

    public void Dead()
    {
        currentHealth = 0;
        gameObject.SetActive(false);
    }
}
