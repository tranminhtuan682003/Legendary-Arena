using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : IState
{
    private SoldierController soldier;

    public SoldierAttackState(SoldierController soldier)
    {
        this.soldier = soldier;
    }

    public void Enter()
    {
        Debug.Log("Nhập trạng thái tấn công");
    }

    public void Execute()
    {
        // unfinished
        if (!soldier.isAttacking)
        {
            soldier.ChangeState(new SoldierMoveState(soldier));
        }
    }

    public void Exit()
    {
        Debug.Log("Rời khỏi trạng thái tấn công");
    }
}
