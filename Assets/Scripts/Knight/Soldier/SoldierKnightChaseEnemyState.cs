using UnityEngine;

public class SoldierKnightChaseEnemyState : IState
{
    private SoldierKnightBase soldierKnightBase;
    private GameObject enemy;
    public SoldierKnightChaseEnemyState(SoldierKnightBase soldierKnightBase, GameObject enemy)
    {
        this.soldierKnightBase = soldierKnightBase;
        this.enemy = enemy;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        soldierKnightBase.ChaseEnemy(enemy);
    }

    public void Exit()
    {
    }
}
