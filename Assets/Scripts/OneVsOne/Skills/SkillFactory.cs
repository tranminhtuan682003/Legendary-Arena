using System;
using UnityEngine;

public static class SkillFactory
{
    public static ISkill CreateSkill(SkillConfig.SkillData skillData, HeroBase hero)
    {
        ISkill skill = null;
        Type skillType = Type.GetType(skillData.skillName);
        GameObject skillObject = new GameObject(skillData.skillName);
        skill = skillObject.AddComponent(skillType) as ISkill;
        if (skill != null)
        {
            skill.SetHero(hero);
            Debug.Log($"{skillData.skillName} has been created and assigned to {hero.name}.");
        }
        return skill;
    }
}
