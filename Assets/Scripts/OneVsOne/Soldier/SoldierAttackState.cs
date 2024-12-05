using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : IState
{
    private SoldierBase soldier;
    public SoldierAttackState(SoldierBase soldier)
    {
        this.soldier = soldier;
    }
    public void Enter()
    {
    }

    public void Execute()
    {
        soldier.HandleAttack();
    }

    public void Exit()
    {
    }
}