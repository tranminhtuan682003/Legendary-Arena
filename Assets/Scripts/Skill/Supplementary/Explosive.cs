using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Ability
{
    private PlayerController playerController;
    public Explosive(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "Explosive";
        cooldown = 50f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
    }
}
