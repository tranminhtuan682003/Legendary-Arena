using Unity.VisualScripting;
using UnityEngine;
public class TurretRed : TurretBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(
            maxHealth: 1000,
            detectionRange: 6f,
            attackRange: 5f,
            attackDamage: 10,
            team: Team.Red
        );
    }

}
