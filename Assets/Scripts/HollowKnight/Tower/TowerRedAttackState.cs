using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRedAttackState : IState
{
    private TowerRedController towerRedController;
    private GameObject enemy;
    private float lastAttackTime = 0f;
    public TowerRedAttackState(TowerRedController towerRedController, GameObject enemy)
    {
        this.towerRedController = towerRedController;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Enter attack state");
    }

    public void Execute()
    {
        towerRedController.FireBullet(enemy, 10f, 1.5f, ref lastAttackTime, 100);
    }

    public void Exit()
    {
    }
}
