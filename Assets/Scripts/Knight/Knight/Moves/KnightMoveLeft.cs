using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveLeft : MoveKnightBase
{
    protected override void Awake()
    {
        base.Awake();
        InitLize(typeMove: TypeMove.Left);
    }
}
