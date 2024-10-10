using UnityEngine;

public class Skill2x : Ability
{
    public Skill2x()
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
