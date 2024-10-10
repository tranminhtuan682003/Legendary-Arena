using UnityEngine;
using System.Collections;

public class GoHome : Ability
{
    private HeroBase hero;
    private Coroutine returnCoroutine; // Lưu lại coroutine để hủy nếu cần

    public GoHome(HeroBase hero)
    {
        this.hero = hero;
        abilityName = "Return";
        cooldown = 0f;
        manaCost = 0f;
    }

    protected override void UseAbility()
    {
        // Nếu không đang return và không di chuyển, bắt đầu return
        if (returnCoroutine == null && !hero.heroParameter.isMoving)
        {
            returnCoroutine = hero.StartCoroutine(ReturnHome());
        }
    }

    IEnumerator ReturnHome()
    {
        float duration = 5f;

        // Gọi hàm ActivateEffect từ hero để kích hoạt hiệu ứng returnHomeEffect
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
