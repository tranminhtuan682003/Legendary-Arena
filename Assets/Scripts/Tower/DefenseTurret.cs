using Unity.VisualScripting;
using UnityEngine;
public class DefenseTurret : TurretBase
{
    protected override void Start()
    {
        base.Start();
        Initialize(2000, 6f, 5f, 50);
    }
    protected override void AttackTarget(HeroBase target)
    {
        throw new System.NotImplementedException();
    }
}
