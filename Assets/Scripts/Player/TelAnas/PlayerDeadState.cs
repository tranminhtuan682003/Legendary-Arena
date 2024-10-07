using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IState
{
    private PlayerController playerController;

    public PlayerDeadState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.ChangeAnimator("Dead");
    }

    public void Execute()
    {
        playerController.Dead();
    }

    public void Exit()
    {
        Debug.Log("Exit Dead State");
    }
}
