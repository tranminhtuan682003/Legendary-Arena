using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "Skills/SkillConfig")]
public class SkillConfig : ScriptableObject
{
    [Serializable]
    public class SkillData
    {
        public string skillName;
        public int skillLevel = 0;
        public float baseDamage;
        public float damagePerLevel;
        public GameObject skillEffect;
        public AudioClip soundEffect;
    }
    public string heroName;
    public SkillData[] defaultSkills;
    public SkillData extraSkill;
}
