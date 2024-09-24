using UnityEngine;

public class MonsterDeadState : IState
{
    private MonsterController monster;

    public MonsterDeadState(MonsterController monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("Enter DeadState");
    }

    public void Execute()
    {
        // Implement dead logic here
    }

    public void Exit()
    {
        Debug.Log("Exit DeadState");
    }
}
