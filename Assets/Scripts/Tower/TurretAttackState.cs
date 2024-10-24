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
        Debug.Log("Trụ chuyển sang trạng thái tấn công.");
    }

    public void Execute()
    {
        turret.HandleAttack();
    }

    public void Exit()
    {
        Debug.Log("Rời trạng thái tấn công.");
    }
}
