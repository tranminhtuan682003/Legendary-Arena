using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
public class Skill1 : SkillBase
{
    protected override void Start()
    {
        base.Start();
        cooldownTime = 6f;
        radius = 155f;
    }

    protected override void SetLabelVisibility(bool isVisible)
    {
        base.SetLabelVisibility(isVisible);
        nameLabel.text = "Up range";

    }
    protected override void FuncitionInOnPointerDown()
    {
        base.FuncitionInOnPointerDown();
        StartCoroutine(IncreaseDistance());
    }

    IEnumerator IncreaseDistance()
    {
        hero.SetMaxRange(3.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed * 0.7f);
        hero.ActivateLightEffect();

        yield return new WaitForSeconds(4f);

        hero.SetMaxRange(2.5f);
        hero.AdjustSpeedShoot(hero.heroParameter.attackSpeed / 0.7f);
        hero.DeactivateLightEffect();
    }
}
