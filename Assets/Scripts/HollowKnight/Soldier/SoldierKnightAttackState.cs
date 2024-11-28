using UnityEngine;

public class SoldierKnightAttackState : IState
{
    private SoldierKnightBase soldierKnightBase;
    private GameObject enemy;
    private float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    public SoldierKnightAttackState(SoldierKnightBase soldierKnightBase, GameObject enemy)
    {
        this.soldierKnightBase = soldierKnightBase;
        this.enemy = enemy;
    }

    public void Enter()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            soldierKnightBase.FireBullet(enemy);
            lastAttackTime = Time.time;
        }
    }

    public void Execute()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            soldierKnightBase.FireBullet(enemy);
            lastAttackTime = Time.time;
        }
    }

    public void Exit()
    {
    }
}
