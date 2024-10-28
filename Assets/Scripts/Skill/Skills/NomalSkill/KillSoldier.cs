using UnityEngine;
public class KillSoldier : SkillBase
{
    public override void Execute()
    {
        hero.ChangeState(new HeroAttackState(hero));
    }
}
