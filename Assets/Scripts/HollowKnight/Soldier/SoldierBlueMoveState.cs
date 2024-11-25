using UnityEngine;

public class SoldierBlueMoveState : IState
{
    private SoldierBlueController soldierBlueController;
    public SoldierBlueMoveState(SoldierBlueController soldierBlueController)
    {
        this.soldierBlueController = soldierBlueController;
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
