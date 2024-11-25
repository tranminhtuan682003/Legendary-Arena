using UnityEngine;

public class SoldierRedAttackState : IState
{
    private SoldierRedController soldier;
    private GameObject enemy;
    private float lastAttackTime;
    private float attackCooldown = 1.5f;

    public SoldierRedAttackState(SoldierRedController soldier, GameObject enemy)
    {
        this.soldier = soldier;
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("SoldierRed entered Attack state.");
        lastAttackTime = Time.time;  // Khởi tạo thời gian tấn công ban đầu
    }

    public void Execute()
    {
        if (enemy == null || !enemy.activeInHierarchy)
        {
            soldier.ChangeState(new SoldierRedMoveState(soldier));  // Nếu kẻ thù không còn tồn tại, quay lại di chuyển
            return;
        }

        // Kiểm tra cooldown và bắn đạn nếu đủ thời gian
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Logic tấn công ở đây (ví dụ: bắn đạn)
            lastAttackTime = Time.time;  // Cập nhật thời gian tấn công
            Debug.Log("SoldierRed is attacking!");
        }
    }

    public void Exit()
    {
        Debug.Log("SoldierRed exited Attack state.");
    }
}
