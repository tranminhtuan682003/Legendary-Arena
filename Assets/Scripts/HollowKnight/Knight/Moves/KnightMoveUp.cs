using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveUp : MoveKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(typeMove: TypeMove.Up);
    }
}
