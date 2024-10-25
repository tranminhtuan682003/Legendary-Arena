using Unity.VisualScripting;
using UnityEngine;
public class TurretRed : TurretBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            maxHealth: 6000,
            detectionRange: 6f,
            attackRange: 5f,
            attackDamage: 100,
            tagEnemy: "PlayerBlue",
            tagSoldierEnemy: "SoldierBlue"

        );
    }

}
