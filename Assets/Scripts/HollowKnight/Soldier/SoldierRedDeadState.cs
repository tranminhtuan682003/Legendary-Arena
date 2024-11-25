using UnityEngine;

public class SoldierRedDeadState : IState
{
    private SoldierRedController soldierRedController;
    public SoldierRedDeadState(SoldierRedController soldierRedController)
    {
        this.soldierRedController = soldierRedController;
    }
    public void Enter()
    {
        // soldierRedController.Dead();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
