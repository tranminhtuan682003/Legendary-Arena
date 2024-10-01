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
        playerController.ChangeAnimator("Idle");
    }

    public void Execute()
    {
        if (playerController.moveDirection != Vector2.zero && !playerController.isDead)
        {
            playerController.ChangeState(new PlayerMoveState(playerController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit ilde state");
    }
}
