using System;
using UnityEngine;

public class TurretEventManager : MonoBehaviour
{
    public static event Action<Transform> OnTargetDetected;
    public static void TriggerTargetDetected(Transform target)
    {
        OnTargetDetected?.Invoke(target);
    }
}
