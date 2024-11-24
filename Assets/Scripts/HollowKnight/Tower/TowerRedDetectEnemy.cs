using UnityEngine;

public class TowerRedDetectEnemy : TowerDetectEnemyBase
{
    private TowerRedController towerController;

    private void Awake()
    {
        // Lấy tham chiếu đến TowerRedController của trụ hiện tại
        towerController = GetComponentInParent<TowerRedController>();
    }

    protected override void OnEnemyDetected(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Blue)
        {
            // Gọi hàm xử lý của TowerRedController khi phát hiện kẻ địch
            towerController.HandleDetectEnemy(enemy, enemyTeam);
        }
    }

    protected override void OnEnemyLost(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Blue)
        {
            // Gọi hàm xử lý của TowerRedController khi kẻ địch rời khỏi phạm vi
            towerController.HandleDetectEnemy(null, Team.None);
        }
    }

    protected override Team GetTeam()
    {
        // Trả về team của tháp
        return Team.Red;
    }
}
