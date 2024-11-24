using UnityEngine;

public class TowerBlueDetectEnemy : TowerDetectEnemyBase
{
    private TowerBlueController towerController;

    private void Awake()
    {
        towerController = GetComponentInParent<TowerBlueController>();
    }

    protected override void OnEnemyDetected(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Red)
        {
            towerController.HandleDetectEnemy(enemy, enemyTeam);
        }
    }

    protected override void OnEnemyLost(GameObject enemy, Team enemyTeam)
    {
        if (enemyTeam == Team.Red)
        {
            towerController.HandleDetectEnemy(null, Team.None);
        }
    }

    protected override Team GetTeam()
    {
        return Team.Blue;
    }
}
