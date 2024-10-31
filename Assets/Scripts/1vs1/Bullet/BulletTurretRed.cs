using UnityEngine;

public class BulletTurretRed : BulletBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        TurretEventManager.OnTargetDetected += OnTargetDetected;
    }
}
