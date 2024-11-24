using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueAttackState : IState
{
    private TowerBlueController towerBlueController;
    private GameObject enemy;
    private float lastAttackTime = 0f;
    public TowerBlueAttackState(TowerBlueController towerBlueController, GameObject enemy)
    {
        this.towerBlueController = towerBlueController;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter attack state");
    }

    public void Execute()
    {
        towerBlueController.FireBullet(enemy, 10f, 1.5f, ref lastAttackTime, 100);
    }

    public void Exit()
    {
    }
}
