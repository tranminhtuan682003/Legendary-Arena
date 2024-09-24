using UnityEngine;

public class MonsterAttackState : IState
{
    private MonsterController monster;

    public MonsterAttackState(MonsterController monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("Enter AttackState");
    }

    public void Execute()
    {
        // Implement attack logic here
    }

    public void Exit()
    {
        Debug.Log("Exit AttackState");
    }
}
