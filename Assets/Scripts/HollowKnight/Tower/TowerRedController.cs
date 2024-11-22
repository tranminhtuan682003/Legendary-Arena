using UnityEngine;
using Zenject;

public class TowerRedController : MonoBehaviour, ITeamMember, IEnemy
{
    private Team team;
    private IState currentState;
    private Transform spawnPoint;

    private int currentHealth;
    private int maxHealth;

    private GameObject currentEnemy;
    private HealthBarEnemyController healthBarController;

    [Inject]
    private UIKnightManager uIKnightManager;

    private void Awake()
    {
        InitLize();

        // Tự động tìm và gán HealthBarEnemyController
        healthBarController = GetComponentInChildren<HealthBarEnemyController>();
        if (healthBarController != null)
        {
            healthBarController.SetEnemy(this);
        }
        else
        {
            Debug.LogError($"HealthBarEnemyController not found for {name}");
        }
    }

    private void Start()
    {
        ChangeState(new TowerRedIdleState(this));
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
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    #region Handle Attack

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        float normalizedHealth = GetCurrentHealth();

        // Cập nhật thanh máu
        if (healthBarController != null)
        {
            healthBarController.UpdateHealth(normalizedHealth);
        }

        Debug.Log($"Tower {name} health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void HandleDetectEnemy(GameObject enemy, Team team)
    {
        if (team == Team.Blue)
        {
            currentEnemy = enemy; // Gán kẻ địch hiện tại
            ChangeState(new TowerRedAttackState(this, enemy));
        }
        else
        {
            currentEnemy = null;
            ChangeState(new TowerRedIdleState(this));
        }
    }

    public void FireBullet(GameObject enemy, float bulletSpeed, float cooldown, ref float lastAttackTime, int bulletDamage)
    {
        if (enemy == null || !enemy.activeInHierarchy) return;

        if (Time.time >= lastAttackTime + cooldown)
        {
            var bullet = uIKnightManager.GetBulletTowerRed(spawnPoint);
            var bulletController = bullet.GetComponent<BulletTowerRedController>();
            if (bulletController != null)
            {
                bulletController.Initialize(enemy, bulletSpeed, bulletDamage);
            }
            lastAttackTime = Time.time;
            Debug.Log("Bullet fired!");
        }
    }

    #endregion

    public void Dead()
    {
        gameObject.SetActive(false);
        Debug.Log($"Tower {name} is destroyed.");
    }
}
