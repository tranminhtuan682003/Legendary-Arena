using UnityEngine;
public class Marksman : Heros
{
    private HeroBase hero;
    public Marksman(HeroBase hero)
    {
        this.hero = hero;
        heroName = "Marksman";
        SetupAbilities();
    }

    public override void SetupAbilities()
    {
        abilities.Add(new NomalAttackx(hero));
        abilities.Add(new Skill1x(hero));
        abilities.Add(new Skill2x());
        abilities.Add(new Skill3x());
        abilities.Add(new GoHome(hero));
        abilities.Add(new Heal(hero));
        abilities.Add(new Explosive(hero));
        abilities.Add(new Farming(hero));
        abilities.Add(new Pushing(hero));
    }
}
