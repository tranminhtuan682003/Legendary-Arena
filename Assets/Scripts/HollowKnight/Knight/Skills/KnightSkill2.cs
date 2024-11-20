using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill2 : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(cooldown: 7f, typeSkill: TypeSkill.Skill2, executionTime: 5 / 6f);
    }
}
