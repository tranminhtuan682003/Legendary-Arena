using UnityEngine;

public class IdleState : IState
{
    private HeroController hero;

    public IdleState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.animator.SetTrigger("Idle");
    }

    public void Execute()
    {
        if (hero.HasInput())
        {
            hero.ChangeState(new MoveState(hero));
        }
    }

    public void Exit()
    {
        hero.animator.ResetTrigger("Idle");
    }
}
