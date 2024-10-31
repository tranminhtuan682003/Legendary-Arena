using System;
using UnityEngine;

public static class SoldierEventManager
{
    public static event Action<Transform> OnTargetDetected;
    public static void TriggerTargetDetected(Transform target)
    {
        OnTargetDetected?.Invoke(target);
    }
}
