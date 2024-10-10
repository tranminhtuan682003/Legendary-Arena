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

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        StartCoroutine(IncreaseDistance());
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
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
