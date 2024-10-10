using UnityEngine;
public class PlayerAttackState : IState
{
    private HeroBase hero;

    public PlayerAttackState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.Attack();
    }

    public void Execute()
    {
        if (!hero.heroParameter.isAttacking)
        {
            hero.ChangeState(new PlayerIdleState(hero));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit attack state");
    }
}
