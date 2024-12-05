using UnityEngine;

public class TowerKnightAttackState : IState
{
    private TowerKnightBase towerKnightBase;
    private GameObject enemy;
    private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    public TowerKnightAttackState(TowerKnightBase towerKnightBase, GameObject enemy)
    {
        this.towerKnightBase = towerKnightBase;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("enter attackstate");
    }

    public void Execute()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            towerKnightBase.FireBullet(enemy);
            lastAttackTime = Time.time;
        }
    }

    public void Exit()
    {
        // Handle any state exit logic here if necessary
    }
}
