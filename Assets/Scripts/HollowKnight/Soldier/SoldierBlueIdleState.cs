using UnityEngine;

public class SoldierBlueIdleState : IState
{
    private SoldierBlueController soldierBlueController;
    public SoldierBlueIdleState(SoldierBlueController soldierBlueController)
    {
        this.soldierBlueController = soldierBlueController;
    }
    public void Enter()
    {
        soldierBlueController.ChangeState(new SoldierBlueMoveState(soldierBlueController));
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
