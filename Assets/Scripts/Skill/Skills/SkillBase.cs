using UnityEngine;
using System.Collections;
public abstract class SkillBase : MonoBehaviour, ISkill
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
}