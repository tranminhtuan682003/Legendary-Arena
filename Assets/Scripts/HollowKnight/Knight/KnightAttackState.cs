using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttackState : IState
{
    private KnightController knightController;
    public KnightAttackState(KnightController knightController)
    {
        this.knightController = knightController;
    }
    public void Enter()
    {
    }

    public void Execute()
    {
        knightController.HandleAttack();
    }

    public void Exit()
    {
        knightController.ChangeAnimation("Idle");
    }
}
