using UnityEngine;

public class TowerAttackState : IState
{
    private TowerController tower;
    private float attackCooldown = 1.0f;
    private float nextAttackTime = 0f;

    public TowerAttackState(TowerController tower)
    {
        this.tower = tower;
    }

    public void Enter()
    {
        Debug.Log("Trụ chuyển sang trạng thái tấn công.");
    }

    public void Execute()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void Exit()
    {
        Debug.Log("Rời trạng thái tấn công.");
    }
}
