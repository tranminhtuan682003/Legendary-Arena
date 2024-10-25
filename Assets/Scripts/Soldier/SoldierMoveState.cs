using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoveState : IState
{
    private SoldierBase soldier;
    public SoldierMoveState(SoldierBase soldier)
    {
        this.soldier = soldier;
    }
    public void Enter()
    {
        Debug.Log("xsaaesfd");
    }

    public void Execute()
    {
        soldier.HandleMove();
    }

    public void Exit()
    {
        Debug.Log("xsaaesfd");
    }
}
