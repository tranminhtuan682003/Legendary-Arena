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
    protected HealthBarBase healthBarController;
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
        GameObject newEnemy = null;

        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<ITeamMember>();
            if (enemy != null && enemy.GetTeam() == teamEnemy)
            {
                foundEnemy = true;
                newEnemy = collider.gameObject;

                // Kiểm tra xem có cần thay đổi kẻ thù hay không
                if (currentEnemy == null || currentEnemy != newEnemy)
                {
                    // Kiểm tra kẻ thù hiện tại có còn trong phạm vi không
                    if (currentEnemy != null && Vector3.Distance(transform.position, currentEnemy.transform.position) > attackRange)
                    {
                        // Nếu kẻ thù cũ đã ra ngoài phạm vi, thay đổi mục tiêu
                        currentEnemy = newEnemy;

                        // Chỉ chuyển sang trạng thái chase nếu không đang ở trong trạng thái chase
                        if (!(currentState is SoldierKnightChaseEnemyState))
                        {
                            ChangeState(new SoldierKnightChaseEnemyState(this, currentEnemy));
                        }
                    }
                    else if (currentEnemy == null)
                    {
                        // Nếu chưa có kẻ thù, thay đổi mục tiêu và chuyển sang chase
                        currentEnemy = newEnemy;
                        if (!(currentState is SoldierKnightChaseEnemyState))
                        {
                            ChangeState(new SoldierKnightChaseEnemyState(this, currentEnemy));
                        }
                    }
                }
                break; // Không cần tiếp tục tìm kiếm nếu đã tìm thấy kẻ thù
            }
        }

        // Nếu không tìm thấy kẻ thù, chuyển sang trạng thái MoveState
        if (!foundEnemy)
        {
            currentEnemy = null;
            if (!(currentState is SoldierKnightMoveState)) // Không chuyển sang MoveState nếu đã ở trong MoveState
            {
                ChangeState(new SoldierKnightMoveState(this));
            }
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
