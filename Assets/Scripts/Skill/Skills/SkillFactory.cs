using System;
using UnityEngine;

public static class SkillFactory
{
    public static ISkill CreateSkill(SkillConfig.SkillData skillData, HeroBase hero)
    {
        ISkill skill = null;
        Type skillType = Type.GetType(skillData.skillName);
        if (skillType == null)
        {
            Debug.LogError($"Skill type '{skillData.skillName}' not found.");
            return null;
        }
        GameObject skillObject = new GameObject(skillData.skillName);
        skill = skillObject.AddComponent(skillType) as ISkill;

        if (skill != null)
        {
            skill.SetHero(hero);
            Debug.Log($"{skillData.skillName} has been created and assigned to {hero.name}.");
        }
        else
        {
            Debug.LogError($"Failed to create skill: {skillData.skillName}");
        }

        return skill;
    }
}
