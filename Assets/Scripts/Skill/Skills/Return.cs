using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class Return : SkillBase
{
    private Coroutine returnCoroutine;
    protected override void Start()
    {
        base.Start();
        cooldownTime = 0f;
        radius = 115f;
    }

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Home";
    }

    protected override void FuncitionInOnPointerDown()
    {
        base.FuncitionInOnPointerDown();
        if (returnCoroutine == null && !hero.heroParameter.isMoving)
        {
            returnCoroutine = hero.StartCoroutine(ReturnHome());
        }
    }

    IEnumerator ReturnHome()
    {
        float duration = 5f;
        hero.ActivateEffect("returnHomeEffect", hero.transform, duration);
        while (duration > 0)
        {
            if (hero.heroParameter.isMoving || hero.heroParameter.isAttacking)
            {
                CancelReturnHome();
                yield break;
            }

            duration -= Time.deltaTime;
            yield return null;
        }
        CancelReturnHome();
        hero.transform.position = hero.startPosition.assasinPosition;
        Debug.Log("Player returned to start position");

        returnCoroutine = null;
    }

    private void CancelReturnHome()
    {
        if (returnCoroutine != null)
        {
            hero.StopCoroutine(returnCoroutine);
            returnCoroutine = null;
            if (hero.heroParameter.skillEffect.ContainsKey("returnHomeEffect"))
            {
                var returnHomeEffect = hero.heroParameter.skillEffect["returnHomeEffect"];
                returnHomeEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}
