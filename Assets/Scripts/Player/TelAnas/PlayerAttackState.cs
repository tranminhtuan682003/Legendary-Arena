using UnityEngine;
public class PlayerAttackState : IState
{
    private PlayerController playerController;

    public PlayerAttackState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.Attack();
    }

    public void Execute()
    {
        if (!playerController.isAttacking)
        {
            playerController.ChangeState(new PlayerIdleState(playerController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit attack state");
    }
}
