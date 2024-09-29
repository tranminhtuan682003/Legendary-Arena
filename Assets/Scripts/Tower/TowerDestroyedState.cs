using UnityEngine;

public class TowerDestroyedState : IState
{
    private TowerController tower;

    public TowerDestroyedState(TowerController tower)
    {
        this.tower = tower;
    }

    public void Enter()
    {
        Debug.Log("Trụ đã bị phá hủy.");
    }

    public void Execute()
    {
        // Không làm gì khi đã bị phá hủy
    }

    public void Exit()
    {
        Debug.Log("Kết thúc trạng thái bị phá hủy.");
    }
}
