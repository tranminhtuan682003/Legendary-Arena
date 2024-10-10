using UnityEngine;

public class PlayerMoveState : IState
{
    private HeroBase hero;

    public PlayerMoveState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.ChangeAnimator("Run");
    }

    public void Execute()
    {
        hero.Move();
        if (hero.heroParameter.moveDirection == Vector2.zero && !hero.IsDead)
        {
            hero.ChangeState(new PlayerIdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit move state");
    }
}
