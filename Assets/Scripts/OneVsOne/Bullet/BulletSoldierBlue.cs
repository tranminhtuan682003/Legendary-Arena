using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSoldierBlue : BulletBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        SoldierEventManager.OnTargetDetected += OnTargetDetected;
    }
}
