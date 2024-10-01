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
        playerController.ChangeAnimator("Run");
    }

    public void Execute()
    {
        playerController.Move();
        if (playerController.moveDirection == Vector2.zero && !playerController.isDead)
        {
            playerController.ChangeState(new PlayerIdleState(playerController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit move state");
    }
}
