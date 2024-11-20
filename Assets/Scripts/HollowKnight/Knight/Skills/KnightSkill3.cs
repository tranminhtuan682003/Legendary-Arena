using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill3 : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(cooldown: 10f, typeSkill: TypeSkill.Skill3, executionTime: 5 / 6f);
    }
}
