using UnityEngine;

public class SoldierBlueController : SoldierKnightBase
{
    protected override void InitLize()
    {
        base.InitLize();
        team = Team.Blue;
        teamEnemy = Team.Red;
        spawnPoint = transform.Find("SpawnPoint");
        maxHealth = 500;
        currentHealth = maxHealth;
        attackRange = 10f;
        speedMove = 2.5f;
        direction = Vector3.right;
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
            var bulletController = bullet.GetComponent<BulletTowerBlueKnightController>();
            if (bulletController != null)
            {
                bulletController.Initialize(enemy, 20f, 20);
            }
        }
    }
}
