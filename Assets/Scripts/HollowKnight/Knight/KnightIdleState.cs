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
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
