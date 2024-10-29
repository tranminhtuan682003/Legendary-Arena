using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;

public class Return : SkillBase
{
    public override void Execute()
    {
        if (skillData.skillEffect != null)
        {
            hero.ChangeState(new HeroRecallState(hero, Effect()));
        }
    }
    private GameObject Effect()
    {
        return ObjectPool.Instance.GetFromPool(skillData.skillEffect, hero.transform.position, hero.transform.rotation);
    }
}
