using UnityEngine;

public class SoldierBlueDeadState : IState
{
    private SoldierBlueController soldierBlueController;
    public SoldierBlueDeadState(SoldierBlueController soldierBlueController)
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
