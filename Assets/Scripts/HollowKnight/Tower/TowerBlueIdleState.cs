using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueIdleState : IState
{
    private TowerBlueController towerBlueController;
    public TowerBlueIdleState(TowerBlueController towerBlueController)
    {
        this.towerBlueController = towerBlueController;
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
