
using UnityEngine;
public class ShootArrow : Ability
{
    public ShootArrow()
    {
        abilityName = "Shoot";
        cooldown = 0f; // Thời gian hồi chiêu ngắn
        manaCost = 10f;
    }

    protected override void UseAbility()
    {
        Debug.Log("Firing a powerful sniper shot!");
    }
}
