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
        knightController.StopMovement(); // Dừng mọi di chuyển
    }

    public void Execute()
    {
        if (knightController.IsMoving())
        {
            knightController.ChangeState(new KnightMoveState(knightController));
        }
        if (knightController.IsAttacking())
        {
            knightController.ChangeState(new KnightAttackState(knightController));
        }
    }

    public void Exit()
    {
    }
}
