using UnityEngine;

public class MonsterIdleState : IState
{
    private MonsterController monster;

    public MonsterIdleState(MonsterController monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("Enter IdleState");
    }

    public void Execute()
    {
        monster.ReturnHome();
        if (monster.Attack())
        {
            monster.ChangeState(new MonsterMoveState(monster));
        }
    }

    public void Exit()
    {
        Debug.Log("Exit IdleState");
    }
}
