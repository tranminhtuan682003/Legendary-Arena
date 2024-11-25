using UnityEngine;
public class SoldierRedController : MonoBehaviour, ITeamMember
{
    private IState currentState;
    private Team team;

    // Health properties
    private int maxHealth;
    private int currentHealth;
    private HealthBarTeamRedController healthBarController;

    private GameObject currentEnemy;
    public float attackRange;
    private float speedMove;

    private bool isMovingRight = false; // Biến theo dõi hướng di chuyển

    private void Start()
    {
        team = Team.Red;
        maxHealth = 500; // Thiết lập máu tối đa
        currentHealth = maxHealth; // Sức khỏe ban đầu
        speedMove = 3f; // Tốc độ di chuyển

        healthBarController = GetComponentInChildren<HealthBarTeamRedController>();
        if (healthBarController != null)
        {
            healthBarController.SetParrent(this);  // Liên kết health bar với soldier
        }

        ChangeState(new SoldierRedMoveState(this));  // Bắt đầu với trạng thái di chuyển
    }

    private void Update()
    {
        currentState?.Execute();
    }

    // Thực hiện việc đổi trạng thái của soldier
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void HandleEnemyDetection(GameObject enemy, Team team)
    {
        if (enemy != null)
        {
            currentEnemy = enemy;

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= attackRange)
            {
                ChangeState(new SoldierRedAttackState(this, enemy));  // Chuyển sang trạng thái tấn công khi kẻ thù vào tầm
            }
            else
            {
                ChangeState(new SoldierRedChaseEnemyState(this, enemy));  // Đuổi theo khi kẻ thù ở ngoài phạm vi tấn công
            }
        }
        else
        {
            currentEnemy = null;
            ChangeState(new SoldierRedMoveState(this));  // Quay lại trạng thái di chuyển nếu không có kẻ thù
        }
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speedMove;
        if (direction.x < 0) // Nếu di chuyển sang trái
            transform.localScale = new Vector3(-1, 1, 1);  // Quay mặt sang trái
        else if (direction.x > 0) // Nếu di chuyển sang phải
            transform.localScale = new Vector3(1, 1, 1);  // Quay mặt sang phải
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();  // Cập nhật thanh máu khi bị thương

        // Kiểm tra chết hay không
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);  // Tắt đối tượng soldier khi chết
        }
    }

    public void UpdateHealthBar()
    {
        float normalizedHealth = GetCurrentHealth();
        if (healthBarController != null)
        {
            healthBarController.UpdateHealth(normalizedHealth);  // Cập nhật thanh máu
        }
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;  // Trả về tỷ lệ phần trăm máu hiện tại
    }

    public Team GetTeam()
    {
        return team;
    }

    public GameObject GetCurrentEnemy()
    {
        return currentEnemy;
    }
}
