using UnityEngine;

public class SoldierKnightDeadState : IState
{
    private SoldierKnightBase soldierKnightBase;
    public SoldierKnightDeadState(SoldierKnightBase soldierKnightBase)
    {
        this.soldierKnightBase = soldierKnightBase;
    }
    public void Enter()
    {
        soldierKnightBase.Dead();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
