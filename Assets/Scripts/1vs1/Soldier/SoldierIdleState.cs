using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : IState
{
    private SoldierBase soldier;
    public SoldierIdleState(SoldierBase soldier)
    {
        this.soldier = soldier;
    }
    public void Enter()
    {
        soldier.ChangeState(new SoldierMoveState(soldier));
    }

    public void Execute()
    {
        Debug.Log("Execute soldierIdleState");
    }

    public void Exit()
    {
        Debug.Log("Exit soldierIdleState");
    }
}
