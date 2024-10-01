using UnityEngine;
public class SlowDown : Ability
{
    public SlowDown()
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
