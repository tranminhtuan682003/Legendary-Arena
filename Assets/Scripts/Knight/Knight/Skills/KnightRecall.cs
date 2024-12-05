using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRecall : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(
            cooldown: 5 / 6f,
            typeSkill: TypeSkill.Recall,
            executionTime: 5f,
            pathImage: "UI/SkillKnight/PNG/Recall"
            );
    }
}
