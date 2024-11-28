using UnityEngine;

public class TowerKnightDeadState : IState
{
    private TowerKnightBase towerKnightBase;
    public TowerKnightDeadState(TowerKnightBase towerKnightBase)
    {
        this.towerKnightBase = towerKnightBase;
    }
    public void Enter()
    {
        towerKnightBase.Dead();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
