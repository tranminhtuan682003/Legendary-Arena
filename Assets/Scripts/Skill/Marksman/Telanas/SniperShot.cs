using UnityEngine;

public class SniperShot : Ability
{
    public SniperShot()
    {
        abilityName = "Sniper";
        cooldown = 10f;
        manaCost = 50f;
    }

    protected override void UseAbility()
    {
        // Logic của kỹ năng SniperShot
        Debug.Log("Firing a powerful sniper shot!");
    }
}
