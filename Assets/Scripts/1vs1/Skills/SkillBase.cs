using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public abstract class SkillBase : MonoBehaviour, ISkill, IPointerUpHandler
{
    public SkillConfig.SkillData skillData;
    protected HeroBase hero;

    public void SetHero(HeroBase hero) => this.hero = hero;

    public virtual void Execute() { }

    public void SetData(SkillConfig.SkillData data, HeroBase hero)
    {
        skillData = data;
        this.hero = hero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("da nhan skill base");
    }
}