using UnityEngine;
public class PushPillar : SkillBase
{
    public override void Execute()
    {
        hero.ChangeState(new HeroAttackState(hero));
    }
}
