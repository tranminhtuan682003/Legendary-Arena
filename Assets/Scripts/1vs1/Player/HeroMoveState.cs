using UnityEngine;

public class HeroMoveState : IState
{
    private HeroBase hero;

    public HeroMoveState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.isActive = true;
        hero.ChangeAnimation("Run");
    }

    public void Execute()
    {
        hero.HandleMove();
        if (hero.movementVector == Vector3.zero && !hero.IsDead)
        {
            hero.ChangeState(new HeroIdleState(hero));
        }
    }

    public void Exit()
    {
    }
}
