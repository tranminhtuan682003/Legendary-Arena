using System;
using UnityEngine;
public static class HeroEventManager
{
    public static event Action<Transform> OnTargetDetected;
    // hero created
    public static event Action<HeroBase> OnHeroCreated;

    public static void TriggerHeroCreated(HeroBase createdHero)
    {
        OnHeroCreated?.Invoke(createdHero);
    }

    //hero detect target
    public static void TriggerTargetDetected(Transform target)
    {
        OnTargetDetected?.Invoke(target);
    }
}
