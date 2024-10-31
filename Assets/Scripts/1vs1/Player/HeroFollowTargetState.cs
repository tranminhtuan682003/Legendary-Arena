using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollowTargetState : IState
{
    private HeroBase hero;
    public HeroFollowTargetState(HeroBase hero)
    {
        this.hero = hero;
    }
    public void Enter()
    {
        hero.ChangeAnimation("Run");
    }

    public void Execute()
    {
        hero.FollowTarget();
        if (hero.isAttacking || hero.target == null)
        {
            hero.ChangeState(new HeroIdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("exit follow state");
    }
}
