
using UnityEngine;
public class SoldierRedMoveState : IState
{
    private SoldierRedController soldier;

    public SoldierRedMoveState(SoldierRedController soldier)
    {
        this.soldier = soldier;
    }

    public void Enter()
    {
        Debug.Log("SoldierRed entered Move state.");
    }

    public void Execute()
    {
        GameObject enemy = soldier.GetCurrentEnemy();  // Lấy kẻ thù hiện tại

        if (enemy != null)
        {
            // Nếu có kẻ thù, chuyển sang trạng thái đuổi theo
            float distanceToEnemy = Vector3.Distance(soldier.transform.position, enemy.transform.position);
            if (distanceToEnemy <= soldier.attackRange)
            {
                soldier.ChangeState(new SoldierRedAttackState(soldier, enemy));  // Nếu trong phạm vi tấn công, tấn công
            }
            else
            {
                soldier.ChangeState(new SoldierRedChaseEnemyState(soldier, enemy));  // Nếu ngoài phạm vi, đuổi theo kẻ thù
            }
        }
        else
        {
            // Nếu không có kẻ thù, soldier sẽ di chuyển sang trái
            soldier.Move(Vector3.left);  // Di chuyển sang trái
        }
    }

    public void Exit()
    {
        Debug.Log("SoldierRed exited Move state.");
    }
}
