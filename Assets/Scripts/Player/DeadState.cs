using UnityEngine;

public class DeadState : IState
{
    private HeroController hero;

    public DeadState(HeroController hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        if (hero.gameObject.activeInHierarchy)
        {
            hero.animator.Play("Dead_com", 0, 0);
        }
    }


    public void Execute()
    {
        hero.SetStateDie();
    }


    public void Exit()
    {
        hero.Dead();
    }
}
