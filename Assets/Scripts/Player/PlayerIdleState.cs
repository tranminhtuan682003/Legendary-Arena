using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController playerController;

    public PlayerIdleState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle State");
    }

    public void Execute()
    {
        // Nếu có input di chuyển, chuyển sang trạng thái Move
        if (playerController.moveDirection != Vector2.zero && !playerController.isDead)
        {
            playerController.ChangeState(new PlayerMoveState(playerController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
