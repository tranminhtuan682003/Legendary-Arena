using UnityEngine;
public class Skill3 : Ability
{
    public Skill3()
    {
        abilityName = "Slow";
        cooldown = 20f; // Thời gian hồi chiêu ngắn
        manaCost = 10f;
    }

    protected override void UseAbility()
    {
        Debug.Log("Firing a powerful sniper shot!");
    }
}
