using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHeal : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(
        cooldown: 20,
        typeSkill: TypeSkill.Heal,
        executionTime: 5,
        pathImage: "UI/SkillKnight/PNG/cc"
        );
    }
}
