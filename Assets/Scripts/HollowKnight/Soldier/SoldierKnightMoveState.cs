
using UnityEngine;
public class SoldierKnightMoveState : IState
{
    private SoldierKnightBase soldierKnightBase;

    public SoldierKnightMoveState(SoldierKnightBase soldierKnightBase)
    {
        this.soldierKnightBase = soldierKnightBase;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        soldierKnightBase.Move();
    }

    public void Exit()
    {
    }
}
