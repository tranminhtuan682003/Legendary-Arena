using UnityEngine;

public class HerroIdleState : IState
{
    private HeroBase hero;

    public HerroIdleState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.animator.SetFloat("Blend", 0);
    }

    public void Execute()
    {
        if (hero.moveDirection != Vector2.zero && !hero.IsDead)
        {
            hero.ChangeState(new HerroMoveState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit ilde state");
    }
}
