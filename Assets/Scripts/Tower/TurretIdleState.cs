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
        Debug.Log("Turret is now idle.");
        // Logic khi trụ vào trạng thái chờ
    }

    public void Execute()
    {
        // turret.DetectPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State turret");
        // Logic khi thoát khỏi trạng thái chờ (nếu có)
    }
}
