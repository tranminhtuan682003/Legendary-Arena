using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public abstract class AttackBase : MonoBehaviour, IPointerDownHandler
{
    protected HeroBase hero;
    public void SetHero(HeroBase heroInstance)
    {
        hero = heroInstance;
        Debug.Log("Hero được gán từ SkillManager: " + hero.name);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!hero.heroParameter.isAttacking)
        {
            hero.ChangeState(new PlayerAttackState(hero));
        }
    }
}
