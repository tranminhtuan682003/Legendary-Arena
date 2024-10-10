using UnityEngine;
public class Skill3x : Ability
{
    public Skill3x()
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
