using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    private PlayerController playerController;
    public Heal(PlayerController playerController)
    {
        this.playerController = playerController;
        abilityName = "Heal";
        cooldown = 30f;
        manaCost = 0f;
    }
    protected override void UseAbility()
    {
        Debug.Log("Mau con lai la : " + playerController.CurrentHealth);
    }
}
