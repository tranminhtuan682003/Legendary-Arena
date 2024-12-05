using UnityEngine;

public class KnightMoveState : IState
{
    private readonly KnightController knightController;
    private readonly TypeMove typeMove;

    public KnightMoveState(KnightController knightController, TypeMove typeMove)
    {
        this.knightController = knightController;
        this.typeMove = typeMove;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        knightController.HandleMoveState(typeMove);
    }

    public void Exit()
    {
        knightController.StopMovement();
    }
}
