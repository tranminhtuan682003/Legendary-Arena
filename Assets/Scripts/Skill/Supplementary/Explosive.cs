using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Ability
{
    private HeroBase hero;
    public Explosive(HeroBase hero)
    {
        this.hero = hero;
        abilityName = "Explosive";
        cooldown = 50f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
    }
}
