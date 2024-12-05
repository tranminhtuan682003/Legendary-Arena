using UnityEngine;

public class TurretDestroyedState : IState
{
    private TurretBase turret;

    public TurretDestroyedState(TurretBase turret)
    {
        this.turret = turret;
    }

    public void Enter()
    {
        turret.TurretDestroyed();
    }

    public void Execute()
    {
        // Trạng thái trụ đã bị phá hủy, không cần làm gì nhiều trong phương thức này
    }

    public void Exit()
    {
        // Logic khi thoát khỏi trạng thái bị phá hủy, có thể không cần nếu trụ không thể thoát trạng thái này
    }
}