using UnityEngine;
using System.Collections;

public class Skill1x : Ability
{
    private HeroBase hero;

    public Skill1x(HeroBase hero)
    {
        this.hero = hero;
        abilityName = "Rapid";
        cooldown = 5f;
        manaCost = 30f;
    }

    protected override void UseAbility()
    {
        hero.StartCoroutine(IncreaseDistance());
    }
    IEnumerator IncreaseDistance()
    {
        hero.SetMaxRange(3.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed * 0.7f);
        hero.ActivateLightEffect();

        yield return new WaitForSeconds(4f);

        hero.SetMaxRange(2.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed / 0.7f);
        hero.DeactivateLightEffect();
    }
}
