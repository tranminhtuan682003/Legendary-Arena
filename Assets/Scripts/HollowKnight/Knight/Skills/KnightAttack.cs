using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : SkillKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(cooldown: 5 / 6f, typeSkill: TypeSkill.Attack);
    }
}
