using UnityEngine;

public class PlayerIdleState : IState
{
    private HeroBase hero;

    public PlayerIdleState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.ChangeAnimator("Idle");
    }

    public void Execute()
    {
        if (hero.heroParameter.moveDirection != Vector2.zero && !hero.IsDead)
        {
            hero.ChangeState(new PlayerMoveState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit ilde state");
    }
}
