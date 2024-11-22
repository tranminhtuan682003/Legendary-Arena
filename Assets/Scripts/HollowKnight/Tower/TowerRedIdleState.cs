using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRedIdleState : IState
{
    private TowerRedController towerRedController;
    public TowerRedIdleState(TowerRedController towerRedController)
    {
        this.towerRedController = towerRedController;
    }

    public void Enter()
    {
        Debug.Log("Enter idle");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}