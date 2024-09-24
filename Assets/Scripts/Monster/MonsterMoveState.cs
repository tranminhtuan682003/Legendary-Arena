using UnityEngine;

public class MonsterMoveState : IState
{
    private MonsterController monster;

    public MonsterMoveState(MonsterController monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("Enter MoveState");
    }

    public void Execute()
    {
        monster.FollowTarget();
        if (!monster.Attack())
        {
            monster.ChangeState(new MonsterIdleState(monster));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit MoveState");
    }
}
