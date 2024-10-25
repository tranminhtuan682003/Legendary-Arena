using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSoldierBlue : BulletBase
{
    protected override void Start()
    {
        Initialize(
            tagEnemy: "PlayerRed",
            tagSoldierEnemy: "SoldierRed",
            tagTurretEnemy: "TurretRed",
            speedMove: 15f,
            target: GamePlay1vs1Manager.Instance.targetOfSoldier,
            damage: 1000
        );
    }
}
