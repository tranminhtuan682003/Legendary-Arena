using UnityEngine;

public class KnightIdleState : IState
{
    private readonly KnightController knightController;

    public KnightIdleState(KnightController knightController)
    {
        this.knightController = knightController;
    }

    public void Enter()
    {
        knightController.StopMovement();
    }

    public void Execute()
    {
        knightController.UpdateCurrentMove();

        knightController.UpdateCurrentSkill();
        if (knightController.currentMove != TypeMove.None && !knightController.isDead)
        {
            knightController.ChangeState(new KnightMoveState(knightController));
        }
        else if (knightController.currentSkill != TypeSkill.None && !knightController.isDead)
        {
            knightController.ChangeState(new KnightAttackState(knightController));
        }
    }

    public void Exit()
    {
    }
}
