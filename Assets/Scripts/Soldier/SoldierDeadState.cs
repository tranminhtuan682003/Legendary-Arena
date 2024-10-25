using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDeadState : IState
{
    private SoldierBase soldier;
    public SoldierDeadState(SoldierBase soldier)
    {
        this.soldier = soldier;
    }
    public void Enter()
    {
        soldier.Dead();
    }

    public void Execute()
    {
        Debug.Log("asdrdytrfhbisd");
    }

    public void Exit()
    {
        Debug.Log("asdftyrtyhbisd");
    }
}
