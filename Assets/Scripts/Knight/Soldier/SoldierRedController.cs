using UnityEngine;
using Zenject;

public class SoldierRedController : SoldierKnightBase
{
    protected override void InitLize()
    {
        base.InitLize();
        team = Team.Red;
        teamEnemy = Team.Blue;
        spawnPoint = transform.Find("SpawnPoint");
        maxHealth = 1000;
        currentHealth = maxHealth;
        attackRange = 10f;
        speedMove = 2.5f;
        direction = Vector3.left;
        healthBarController = GetComponentInChildren<HealthBarTeamRedController>();
        healthBarController.SetParrent(this);
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
                bulletController.Initialize(enemy, 30f, 20);
            }
        }
    }
}
