using System.Collections;
using UnityEngine;

public class KnightAttackState : IState
{
    private readonly KnightController knightController;

    public KnightAttackState(KnightController knightController)
    {
        this.knightController = knightController;
    }

    public void Enter()
    {
        knightController.SetCooldown();
        knightController.HandleAttack();
    }

    public void Execute()
    {
        knightController.UpdateCurrentSkill();
        if (knightController.currentSkill == TypeSkill.None && !knightController.isDead)
        {
            knightController.StartCoroutine(WaitForExit());
        }
    }

    public void Exit()
    {
        Debug.Log("exit attaaack");
    }

    private IEnumerator WaitForExit()
    {
        float cooldown = knightController.GetCooldown();
        yield return new WaitForSeconds(cooldown);
        knightController.ChangeState(new KnightIdleState(knightController));
    }

}
