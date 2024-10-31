using UnityEngine;

public class TurretIdleState : IState
{
    private TurretBase turret;

    public TurretIdleState(TurretBase turret)
    {
        this.turret = turret;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        // turret.DetectPlayer();
    }

    public void Exit()
    {
    }
}
