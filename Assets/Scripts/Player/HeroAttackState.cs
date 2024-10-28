using System.Collections;
using UnityEngine;
public class HeroAttackState : IState
{
    private HeroBase hero;

    public HeroAttackState(HeroBase hero)
    {
        this.hero = hero;
    }

    public void Enter()
    {
        hero.isActive = true;
        hero.StartCoroutine(ChangeState());
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        Debug.Log("Exit Attack State");
    }

    IEnumerator ChangeState()
    {
        hero.ChangeAnimation("Attack");
        hero.HandleAttack();
        hero.attackCooldown = hero.attackInterval;
        yield return new WaitForSeconds(hero.attackInterval);
        hero.ChangeState(new HeroIdleState(hero));
    }
}
