using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : IState
{
    private SoldierController soldier;

    public SoldierIdleState(SoldierController soldier)
    {
        this.soldier = soldier;
    }

    public void Enter()
    {
        Debug.Log("enter idleSoldierState");
    }

    public void Execute()
    {
        if (soldier.targetPositions.Count > 0)
        {
            soldier.ChangeState(new SoldierMoveState(soldier));
        }
    }

    public void Exit()
    {
        Debug.Log("exit idleSoldierState");
    }
}
