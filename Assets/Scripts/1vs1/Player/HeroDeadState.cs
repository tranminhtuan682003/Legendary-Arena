using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeadState : IState
{
    private HeroBase hero;

    public HeroDeadState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.ChangeAnimation("Dead");
    }

    public void Execute()
    {
        hero.Dead();
    }

    public void Exit()
    {
    }
}
