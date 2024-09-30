using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerController playerController;

    public PlayerMoveState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("Enter Move State");
    }

    public void Execute()
    {
        playerController.Move(); // Gọi phương thức Move() từ PlayerController
    }

    public void Exit()
    {
        Debug.Log("Exit Move State");
    }
}
