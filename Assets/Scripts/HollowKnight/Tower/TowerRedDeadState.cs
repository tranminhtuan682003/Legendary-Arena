using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRedDeadState : IState
{
    private TowerRedController towerRedController;
    public TowerRedDeadState(TowerRedController towerRedController)
    {
        this.towerRedController = towerRedController;
    }

    public void Enter()
    {
        Debug.Log("Enter Dead");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
