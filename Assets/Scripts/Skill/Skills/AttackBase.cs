using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public abstract class AttackBase : MonoBehaviour, IPointerDownHandler
{
    protected HeroBase hero;
    protected virtual void Start()
    {
        hero = FindAnyObjectByType<HeroBase>();

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!hero.heroParameter.isAttacking)
        {
            hero.ChangeState(new PlayerAttackState(hero));
        }
    }
}
