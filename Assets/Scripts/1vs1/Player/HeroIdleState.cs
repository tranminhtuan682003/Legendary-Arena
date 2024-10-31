using UnityEngine;

public class HeroIdleState : IState
{
    private HeroBase hero;

    public HeroIdleState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.isActive = false;
        hero.ChangeAnimation("Idle");
    }

    public void Execute()
    {
        if (hero.movementVector != Vector3.zero && !hero.IsDead)
        {
            hero.ChangeState(new HeroMoveState(hero));
        }
    }

    public void Exit()
    {
    }
}
