using UnityEngine;

public class SoldierRedChaseEnemyState : IState
{
    private SoldierRedController soldier;
    private GameObject enemy;

    public SoldierRedChaseEnemyState(SoldierRedController soldier, GameObject enemy)
    {
        this.soldier = soldier;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("SoldierRed entered Chase state.");
    }

    public void Execute()
    {
        if (enemy != null)
        {
            // Đuổi theo kẻ thù
            Vector3 direction = (enemy.transform.position - soldier.transform.position).normalized;
            soldier.Move(direction);  // Di chuyển về phía kẻ thù

            float distanceToEnemy = Vector3.Distance(soldier.transform.position, enemy.transform.position);
            if (distanceToEnemy <= soldier.attackRange)
            {
                soldier.ChangeState(new SoldierRedAttackState(soldier, enemy));  // Chuyển sang trạng thái tấn công khi đến gần kẻ thù
            }
        }
        else
        {
            soldier.ChangeState(new SoldierRedMoveState(soldier));  // Nếu không có kẻ thù, quay lại trạng thái di chuyển
        }
    }

    public void Exit()
    {
        Debug.Log("SoldierRed exited Chase state.");
    }
}
