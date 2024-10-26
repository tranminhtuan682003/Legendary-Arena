using UnityEngine;

public class HerroMoveState : IState
{
    private HeroBase hero;

    public HerroMoveState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        // hero.ChangeAnimator("Run");
    }

    public void Execute()
    {
        hero.Move();
        if (hero.moveDirection == Vector2.zero && !hero.IsDead)
        {
            hero.ChangeState(new HerroIdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit move state");
    }
}
