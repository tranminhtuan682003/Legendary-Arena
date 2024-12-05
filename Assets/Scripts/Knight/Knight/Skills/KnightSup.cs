using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSup : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(
            cooldown: 1f,
            typeSkill: TypeSkill.Supplymentary,
            executionTime: 5 / 6f,
            pathImage: "UI/SkillKnight/PNG/th"
            );
    }
}
