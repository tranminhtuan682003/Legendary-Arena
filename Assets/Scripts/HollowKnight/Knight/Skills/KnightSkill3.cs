using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSkill3 : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(cooldown: 0.5f, typeSkill: TypeSkill.Skill3);
    }
}
