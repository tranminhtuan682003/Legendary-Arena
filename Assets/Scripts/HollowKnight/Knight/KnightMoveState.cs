using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveState : IState
{
    private readonly KnightController knightController;

    public KnightMoveState(KnightController knightController)
    {
        this.knightController = knightController;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        knightController.HandleMove();

        // Quay về trạng thái Idle nếu không có input
        if (!knightController.IsMoving())
        {
            knightController.ChangeState(new KnightIdleState(knightController));
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
