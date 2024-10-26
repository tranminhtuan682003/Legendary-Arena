using UnityEngine;
public class HerroAttackState : IState
{
    private HeroBase hero;

    public HerroAttackState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        Debug.Log("Exit attack state");
    }
}
