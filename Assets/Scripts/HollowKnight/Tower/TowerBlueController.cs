using UnityEngine;
using Zenject;

public class TowerBlueController : MonoBehaviour, ITeamMember
{
    private Team team;
    private IState currentState;
    private Transform spawnPoint;

    private int currentHealth;
    private int maxHealth;

    private GameObject currentEnemy;
    private HealthBarTeamBlueController healthBarController;

    [Inject]
    private UIKnightManager uIKnightManager;

    private void Awake()
    {
        InitLize();

        // Tự động tìm và gán HealthBarEnemyController
        healthBarController = GetComponentInChildren<HealthBarTeamBlueController>();
        if (healthBarController != null)
        {
            healthBarController.SetParrent(this);
        }
        else
        {
            Debug.LogError($"HealthBarEnemyController not found for {name}");
        }
    }

    private void Start()
    {
        ChangeState(new TowerBlueIdleState(this));
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
        team = Team.Blue;
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
            ChangeState(new TowerBlueDeadState(this));
        }
    }

    public void HandleDetectEnemy(GameObject enemy, Team team)
    {
        Debug.Log("dang ban");
        if (team == Team.Red)
        {
            currentEnemy = enemy; // Gán kẻ địch hiện tại
            ChangeState(new TowerBlueAttackState(this, enemy));
        }
        else
        {
            currentEnemy = null;
            ChangeState(new TowerBlueIdleState(this));
        }
    }

    public void FireBullet(GameObject enemy, float bulletSpeed, float cooldown, ref float lastAttackTime, int bulletDamage)
    {
        if (enemy == null || !enemy.activeInHierarchy) return;

        if (Time.time >= lastAttackTime + cooldown)
        {
            var bullet = uIKnightManager.GetBulletTowerBlue(spawnPoint);
            var bulletController = bullet.GetComponent<BulletTowerBlueKnightController>();
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
