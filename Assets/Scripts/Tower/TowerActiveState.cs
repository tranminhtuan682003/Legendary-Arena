using UnityEngine;

public class TowerActiveState : IState
{
    private TowerController tower;

    public TowerActiveState(TowerController tower)
    {
        this.tower = tower;
    }

    public void Enter()
    {
        Debug.Log("Trụ ở trạng thái hoạt động.");
    }

    public void Execute()
    {
        // Logic khi trụ ở trạng thái hoạt động
    }

    public void Exit()
    {
        Debug.Log("Rời trạng thái hoạt động.");
    }
}
