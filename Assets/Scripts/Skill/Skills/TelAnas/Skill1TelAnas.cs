using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
public class Skill1TelAnas : SkillBase
{
    protected override void Start()
    {
        base.Start();
        cooldownTime = 6f;
        radius = 155f;
    }

    protected override void FuncitionInOnPointerDown()
    {
        base.FuncitionInOnPointerDown();
        StartCoroutine(IncreaseDistance());
    }

    IEnumerator IncreaseDistance()
    {
        if (hero == null)
        {
            Debug.LogError("Hero is not set!");
            yield break; // Thoát khỏi coroutine nếu hero chưa được khởi tạo
        }

        hero.SetMaxRange(3.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed * 0.7f);
        hero.ActivateLightEffect();

        yield return new WaitForSeconds(4f);

        hero.SetMaxRange(2.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed / 0.7f);
        hero.DeactivateLightEffect();
    }

}
