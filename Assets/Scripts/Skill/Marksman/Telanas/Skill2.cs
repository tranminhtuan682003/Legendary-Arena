using UnityEngine;

public class Skill2 : Ability
{
    public Skill2()
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
