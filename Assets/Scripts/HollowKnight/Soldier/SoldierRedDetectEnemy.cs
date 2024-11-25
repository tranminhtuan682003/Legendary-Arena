using UnityEngine;

public class SoldierRedDetectEnemy : SoldierDetectEnemyBase
{
    private SoldierRedController soldierRedController;
    private GameObject currentTarget;

    private void Awake()
    {
        soldierRedController = GetComponentInParent<SoldierRedController>();
    }

    protected override void OnEnemyDetected(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Blue)
        {
            // Chỉ lấy mục tiêu nếu chưa có target
            if (currentTarget == null)
            {
                currentTarget = enemy;
                soldierRedController.HandleEnemyDetection(currentTarget, enemyTeam);
                Debug.Log($"New target acquired: {enemy.name}");
            }
        }
    }

    protected override void OnEnemyLost(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Blue && currentTarget == enemy)
        {
            // Khi mất mục tiêu hiện tại, xóa target
            currentTarget = null;
            soldierRedController.HandleEnemyDetection(null, Team.None);
            Debug.Log("Lost current target.");

            // Tìm mục tiêu tiếp theo trong phạm vi nếu có
            var nextEnemy = GetNextEnemy();
            if (nextEnemy != null)
            {
                currentTarget = nextEnemy;
                soldierRedController.HandleEnemyDetection(currentTarget, Team.Blue);
                Debug.Log($"Switched to new target: {currentTarget.name}");
            }
        }
    }

    protected override Team GetTeam()
    {
        return Team.Red;
    }

    private GameObject GetNextEnemy()
    {
        // Lấy tất cả các enemy trong phạm vi
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<ITeamMember>();
            if (enemy != null && enemy.GetTeam() == Team.Blue && collider.gameObject != currentTarget)
            {
                return collider.gameObject;
            }
        }
        return null;
    }
}
