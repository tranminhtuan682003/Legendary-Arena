using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMoveState : IState
{
    private SoldierController soldier;

    public SoldierMoveState(SoldierController soldier)
    {
        this.soldier = soldier;
    }

    public void Enter()
    {
        Debug.Log("enter moveSoldierState");
    }

    public void Execute()
    {
        soldier.FollowEnemy();

        if (soldier.isAttacking)
        {
            soldier.ChangeState(new SoldierAttackState(soldier));
        }
    }

    public void Exit()
    {
        Debug.Log("exit moveSoldierState");
    }
}
