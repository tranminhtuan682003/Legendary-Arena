using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueDeadState : IState
{
    private TowerBlueController towerBlueController;
    public TowerBlueDeadState(TowerBlueController towerBlueController)
    {
        this.towerBlueController = towerBlueController;
    }
    public void Enter()
    {
        towerBlueController.Dead();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
