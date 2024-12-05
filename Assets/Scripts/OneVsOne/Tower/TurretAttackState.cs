using UnityEngine;

public class TurretAttackState : IState
{
    private TurretBase turret;
    public TurretAttackState(TurretBase turret)
    {
        this.turret = turret;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        turret.HandleAttack();
    }

    public void Exit()
    {
    }
}
