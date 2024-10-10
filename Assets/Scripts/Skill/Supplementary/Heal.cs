using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    private HeroBase hero;
    public Heal(HeroBase hero)
    {
        this.hero = hero;
        abilityName = "Heal";
        cooldown = 30f;
        manaCost = 0f;
    }
    protected override void UseAbility()
    {
        Debug.Log("Mau con lai la : " + hero.CurrentHealth);
    }
}
