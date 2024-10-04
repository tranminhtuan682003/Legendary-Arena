using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Ability
{
    public Explosive()
    {
        abilityName = "Explosive";
        cooldown = 50f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        Debug.Log("Firing a powerful sniper shot!");
    }
}
