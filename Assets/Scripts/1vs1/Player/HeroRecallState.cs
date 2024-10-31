using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRecallState : IState
{
    private HeroBase hero;
    private GameObject effect;
    private Coroutine recallCoroutine;

    public HeroRecallState(HeroBase hero, GameObject effect)
    {
        this.hero = hero;
        this.effect = effect;
    }

    public void Enter()
    {
        recallCoroutine = hero.StartCoroutine(ReturnEffectToPool());
    }

    public void Execute()
    {
        if (hero.movementVector != Vector3.zero && !hero.IsDead)
        {
            hero.ChangeState(new HeroMoveState(hero));
        }
    }

    public void Exit()
    {
        if (recallCoroutine != null)
        {
            hero.StopCoroutine(recallCoroutine);
            recallCoroutine = null;
            effect.SetActive(false);
        }
    }

    private IEnumerator ReturnEffectToPool()
    {
        yield return new WaitForSeconds(8f);
        effect.SetActive(false);
        if (hero.GetTeam() == Team.Blue)
        {
            hero.transform.position = new Vector3(-32, 0, -30.5f);
        }
        else
        {
            hero.transform.position = new Vector3(26.45f, 0f, 27.95f);
        }
    }
}
