using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveDown : MoveKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(typeMove: TypeMove.Down);
    }
}
