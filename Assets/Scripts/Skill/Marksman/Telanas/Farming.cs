public class Farming : Ability
{
    private HeroBase hero;
    public Farming(HeroBase hero)
    {
        this.hero = hero;
        abilityName = "";
        cooldown = 0f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        if (!hero.heroParameter.isAttacking)
        {
            hero.ChangeState(new PlayerAttackState(hero));
        }
    }
}
