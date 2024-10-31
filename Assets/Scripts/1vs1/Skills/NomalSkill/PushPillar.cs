using UnityEngine;
public class PushPillar : SkillBase
{
    public override void Execute()
    {
        base.Execute();
        if (hero.attackCooldown <= 0)
        {
            if (hero.target && hero.isAttacking)
            {
                hero.ChangeState(new HeroAttackState(hero, Effect()));
            }
            else if (hero.target != null && !hero.isAttacking)
            {
                hero.ChangeState(new HeroFollowTargetState(hero));
            }
            else if (!hero.target && !hero.isAttacking)
            {
                hero.ChangeState(new HeroAttackState(hero, Effect()));
            }
        }
    }

    private GameObject Effect()
    {
        return ObjectPool.Instance.GetFromPool(skillData.skillEffect, hero.spawnPoint.position, hero.spawnPoint.rotation);
    }
}
