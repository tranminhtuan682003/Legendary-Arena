using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IState
{
    private HeroBase hero;

    public PlayerDeadState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.ChangeAnimator("Dead");
    }

    public void Execute()
    {
        hero.Dead();
    }

    public void Exit()
    {
        Debug.Log("Exit Dead State");
    }
}
