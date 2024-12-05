using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDeadState : IState
{
    private KnightController knightController;
    public KnightDeadState(KnightController knightController)
    {
        this.knightController = knightController;
    }
    public void Enter()
    {
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

}
