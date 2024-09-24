using UnityEngine;

public class MoveState : IState
{
    private HeroController hero;

    public MoveState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.animator.SetTrigger("Walking");
    }

    public void Execute()
    {
        hero.Move();
        if (!hero.HasInput())
        {
            hero.ChangeState(new IdleState(hero));
        }
    }

    public void Exit()
    {
        hero.animator.ResetTrigger("Walking");
    }
}

