using System;
using UnityEngine;
public static class TargetingEventManager
{
    public static event Action<Transform> OnTargetDetected;
    public static event Action OnTargetLost;
    public static event Action OnStartAttacking;
    public static event Action OnStopAttacking;

    public static void TriggerTargetDetected(Transform target)
    {
        OnTargetDetected?.Invoke(target);
    }

    public static void TriggerTargetLost()
    {
        OnTargetLost?.Invoke();
    }

    public static void TriggerStartAttacking()
    {
        OnStartAttacking?.Invoke();
    }

    public static void TriggerStopAttacking()
    {
        OnStopAttacking?.Invoke();
    }
}
