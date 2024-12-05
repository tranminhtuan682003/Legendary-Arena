using UnityEngine;

public class MainTowerBlueController : TowerKnightBase
{
    protected override void Initialize()
    {
        // Gọi lại phương thức khởi tạo của lớp cha
        base.Initialize();
        team = Team.Blue;
        teamEnemy = Team.Red;
        spawnPoint = transform.Find("SpawnPoint");
        maxHealth = 2000;
        currentHealth = maxHealth;
        attackRange = 10f;
        healthBarController = GetComponentInChildren<HealthBarTeamRedController>();
        healthBarController.SetParrent(this); // Đặt reference cho health bar
    }

    public override void FireBullet(GameObject enemy)
    {
        base.FireBullet(enemy);
        if (enemy == null) return;
        var bullet = uIKnightManager.GetBulletTowerRed(spawnPoint);
        if (bullet != null)
        {
            var bulletController = bullet.GetComponent<BulletTowerRedKnightController>();
            if (bulletController != null)
            {
                bulletController.Initialize(enemy, 20f, 50);
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        KnightEventManager.InvokeGameWin();
    }
}
