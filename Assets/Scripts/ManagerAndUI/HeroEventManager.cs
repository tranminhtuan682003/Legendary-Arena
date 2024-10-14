using System;
public static class HeroEventManager
{
    public static event Action<HeroBase> OnHeroCreated;

    public static void TriggerHeroCreated(HeroBase createdHero)
    {
        OnHeroCreated?.Invoke(createdHero);
    }
}
