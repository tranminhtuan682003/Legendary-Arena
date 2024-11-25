using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class SoldierBlueController : MonoBehaviour, ITeamMember
{
    private IState currentState;
    [Inject] private UIKnightManager uIKnightManager;

    [Header("Team Info")]
    private Team team;

    [Header("Health")]
    private int maxHealth;
    private int currentHealth;

    [Header("Move")]
    private float speedMove;

    [Header("Attack")]
    private GameObject currentEnemy;
    private Transform spawnPoint;
    private float lastAttackTime;

    private void Awake()
    {
        InitSoldier();
    }

    private void Start()
    {
        ChangeState(new SoldierBlueIdleState(this));
    }

    private void Update()
    {
        currentState?.Execute();
    }

    public void InitSoldier()
    {
        team = Team.Blue;
        spawnPoint = transform.Find("SpawnPoint");
        maxHealth = 500;
        currentHealth = maxHealth;
        speedMove = 3f;
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void HandleEnemyDetection(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Red)
        {
            currentEnemy = enemy;
            ChangeState(new SoldierBlueAttackState(this, currentEnemy));
        }
        else
        {
            currentEnemy = null;
            ChangeState(new SoldierBlueIdleState(this));
        }
    }

    public void FireBullet(GameObject enemy, float bulletSpeed, float cooldown, int bulletDamage)
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
            Debug.Log("SoldierBlue fired a bullet!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            ChangeState(new SoldierBlueDeadState(this));
        }
    }

    public Team GetTeam()
    {
        return team;
    }

    public float GetCurrentHealth()
    {
        return (float)currentHealth / maxHealth;
    }
}
