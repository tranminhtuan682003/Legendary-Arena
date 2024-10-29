using UnityEngine;
public class NomalAttack : SkillBase
{
    public override void Execute()
    {
        if (hero.attackCooldown <= 0)
        {
            if (hero.target && hero.isAttacking)
            {
                hero.ChangeState(new HeroAttackState(hero));
            }
            else if (hero.target != null && !hero.isAttacking)
            {
                hero.ChangeState(new HeroFollowTargetState(hero));
            }
            else if (!hero.target && !hero.isAttacking)
            {
                hero.ChangeState(new HeroAttackState(hero));
            }
        }
    }
}
