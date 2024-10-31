using System.Collections;
using UnityEngine;
public class HeroAttackState : IState
{
    private HeroBase hero;
    private GameObject effect;
    public HeroAttackState(HeroBase hero, GameObject effect)
    {
        this.hero = hero;
        this.effect = effect;
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
        effect.SetActive(false);
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
