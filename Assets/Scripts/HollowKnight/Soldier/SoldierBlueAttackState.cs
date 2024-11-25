using UnityEngine;

public class SoldierBlueAttackState : IState
{
    private SoldierBlueController soldier;
    private GameObject enemy;
    private float bulletSpeed = 10f;
    private float cooldown = 1.5f;
    private int bulletDamage = 100;

    public SoldierBlueAttackState(SoldierBlueController soldier, GameObject enemy)
    {
        this.soldier = soldier;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("SoldierBlue entered Attack state.");
    }

    public void Execute()
    {
        soldier.FireBullet(enemy, bulletSpeed, cooldown, bulletDamage);
    }

    public void Exit()
    {
        Debug.Log("SoldierBlue exited Attack state.");
    }
}
