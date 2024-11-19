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
        knightController.PlayAnimation("Run");
    }

    public void Execute()
    {
        knightController.UpdateCurrentMove();
        knightController.UpdateCurrentSkill();

        knightController.HandleMovement();

        if (knightController.currentMove == TypeMove.None && !knightController.isDead)
        {
            knightController.ChangeState(new KnightIdleState(knightController));
        }
        else if (knightController.currentSkill != TypeSkill.None && !knightController.isDead)
        {
            knightController.ChangeState(new KnightAttackState(knightController));
        }
    }

    public void Exit()
    {
        knightController.StopMovement();
    }
}
