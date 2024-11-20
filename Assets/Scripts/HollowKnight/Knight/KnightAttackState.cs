using System.Collections;
using UnityEngine;

public class KnightAttackState : IState
{
    private readonly KnightController knightController;
    private readonly TypeSkill typeSkill;
    private readonly float cooldown;
    private readonly float executionTime;

    public KnightAttackState(KnightController knightController, TypeSkill typeSkill, float cooldown, float executionTime)
    {
        this.knightController = knightController;
        this.typeSkill = typeSkill;
        this.cooldown = cooldown;
        this.executionTime = executionTime;
    }

    public void Enter()
    {
        knightController.HandleAttackState(typeSkill, cooldown);
        knightController.SetSkillCooldown(typeSkill, cooldown);
        knightController.StartCoroutine(TransitionToIdleAfterExecution());
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

    private IEnumerator TransitionToIdleAfterExecution()
    {
        yield return new WaitForSeconds(executionTime);
        knightController.isExecuting = false;
        knightController.ChangeState(new KnightIdleState(knightController));
        knightController.PlayAnimation("Idle");
    }
}
