using UnityEngine;

public class BulletTurretRed : BulletBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            tagEnemy: "PlayerBlue",
            tagSoldierEnemy: "SoldierBlue",
            tagTurretEnemy: "TurretBlue",
            speedMove: 20f,
            target: GamePlay1vs1Manager.Instance.targetOfTurret,
            damage: 1

        );
    }
}
