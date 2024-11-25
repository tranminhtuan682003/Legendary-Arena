using UnityEngine;

public class SoldierRedIdleState : IState
{
    private SoldierRedController soldierRedController;
    public SoldierRedIdleState(SoldierRedController soldierRedController)
    {
        this.soldierRedController = soldierRedController;
    }
    public void Enter()
    {
        soldierRedController.ChangeState(new SoldierRedMoveState(soldierRedController));
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
