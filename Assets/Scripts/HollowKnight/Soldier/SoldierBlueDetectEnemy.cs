using UnityEngine;

public class SoldierBlueDetectEnemy : SoldierDetectEnemyBase
{
    private SoldierBlueController soldierBlueController;
    private GameObject currentTarget;

    private void Awake()
    {
        soldierBlueController = GetComponentInParent<SoldierBlueController>();
    }

    protected override void OnEnemyDetected(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Red)
        {
            // Chỉ lấy mục tiêu nếu chưa có target
            if (currentTarget == null)
            {
                currentTarget = enemy;
                soldierBlueController.HandleEnemyDetection(currentTarget, enemyTeam);
                Debug.Log($"New target acquired: {enemy.name}");
            }
        }
    }

    protected override void OnEnemyLost(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Red && currentTarget == enemy)
        {
            // Khi mất mục tiêu hiện tại, xóa target
            currentTarget = null;
            soldierBlueController.HandleEnemyDetection(null, Team.None);
            Debug.Log("Lost current target.");

            // Tìm mục tiêu tiếp theo trong phạm vi nếu có
            var nextEnemy = GetNextEnemy();
            if (nextEnemy != null)
            {
                currentTarget = nextEnemy;
                soldierBlueController.HandleEnemyDetection(currentTarget, Team.Red);
                Debug.Log($"Switched to new target: {currentTarget.name}");
            }
        }
    }

    protected override Team GetTeam()
    {
        return Team.Blue;
    }

    private GameObject GetNextEnemy()
    {
        // Lấy tất cả các enemy trong phạm vi
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<ITeamMember>();
            if (enemy != null && enemy.GetTeam() == Team.Red && collider.gameObject != currentTarget)
            {
                return collider.gameObject;
            }
        }
        return null;
    }
}
