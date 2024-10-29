public class BulletSoldierRed : BulletBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SoldierEventManager.OnTargetDetected += OnTargetDetected;
    }
}
