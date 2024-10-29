using System.Collections;
using UnityEngine;

public class BulletValhein : BulletBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        HeroEventManager.OnTargetDetected += OnTargetDetected;
    }
}
