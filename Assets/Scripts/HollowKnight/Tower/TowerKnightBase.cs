using UnityEngine;
using Zenject;

public class TowerKnightBase : MonoBehaviour, ITeamMember
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
    protected Transform spawnPoint;

    private void OnEnable()
    {
        Initialize();
    }

    private void Start()
    {
        // Khởi tạo trạng thái ban đầu
        ChangeState(new TowerKnightIdleState(this));
    }

    private void Update()
    {
        currentState?.Execute();
        DetectEnemyAndHandle();
    }

    // Phương thức khởi tạo, có thể ghi đè trong các lớp con
    protected virtual void Initialize()
    {
        // Thực hiện các thao tác khởi tạo mặc định hoặc cho các lớp con ghi đè
    }

    // Thay đổi trạng thái của đối tượng
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // Kiểm tra các đối tượng trong phạm vi tấn công và thay đổi trạng thái
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

                // Nếu có kẻ thù mới và không phải là kẻ thù hiện tại
                if (currentEnemy != newEnemy)
                {
                    currentEnemy = newEnemy;

                    // Chỉ chuyển trạng thái sang AttackState nếu chưa ở trong AttackState
                    if (!(currentState is TowerKnightAttackState))
                    {
                        ChangeState(new TowerKnightAttackState(this, currentEnemy));
                    }
                }
                break; // Không cần kiểm tra thêm sau khi đã tìm thấy kẻ thù
            }
        }

        // Nếu không tìm thấy kẻ thù, chuyển sang trạng thái Idle
        if (!foundEnemy)
        {
            currentEnemy = null;

            // Chỉ chuyển sang IdleState nếu chưa ở trong IdleState
            if (!(currentState is TowerKnightIdleState))
            {
                ChangeState(new TowerKnightIdleState(this));
            }
        }
    }



    // Phương thức này sẽ được kế thừa trong các lớp con để thực hiện hành động tấn công cụ thể
    public virtual void FireBullet(GameObject enemy) { }

    // Phương thức nhận sát thương
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            ChangeState(new TowerKnightDeadState(this)); // Chuyển sang trạng thái chết
        }
    }

    // Cập nhật thanh máu
    public void UpdateHealthBar()
    {
        float normalizedHealth = GetCurrentHealth();
        healthBarController.UpdateHealth(normalizedHealth);
    }

    // Tỷ lệ máu còn lại
    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    // Lấy team của đối tượng
    public Team GetTeam()
    {
        return team;
    }

    // Xử lý khi đối tượng chết
    public void Dead()
    {
        currentHealth = 0;
        gameObject.SetActive(false); // Tắt đối tượng khi chết
    }
}
