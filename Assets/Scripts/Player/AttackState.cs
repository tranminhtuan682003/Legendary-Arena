using UnityEngine;

public class AttackState : IState
{
    private HeroController hero;
    private float attackAnimationDuration;
    private bool isAnimationFinished;

    public AttackState(HeroController hero)
    {
        this.hero = hero;
        // Assuming the animation clip's length is stored somewhere or you know its duration
        // Here you would fetch the duration dynamically if needed
        attackAnimationDuration = 1.0f; // Set to the duration of your attacking animation
        isAnimationFinished = false;
    }

    public void Enter()
    {
        Debug.Log("enter attack");
        hero.animator.SetTrigger("Attacking");
        isAnimationFinished = false;
    }

    public void Execute()
    {
        // Check if animation is finished
        AnimatorStateInfo stateInfo = hero.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attacking") && stateInfo.normalizedTime >= 1.0f)
        {
            isAnimationFinished = true;
        }

        if (!hero.attacking || isAnimationFinished)
        {
            hero.ChangeState(new IdleState(hero));
        }
    }

    public void Exit()
    {
        hero.animator.ResetTrigger("Attacking");
        Debug.Log("exit attack");
    }
}
