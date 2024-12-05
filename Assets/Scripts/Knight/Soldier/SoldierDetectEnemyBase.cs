using UnityEngine;

public abstract class SoldierDetectEnemyBase : MonoBehaviour
{
    private Team teamEnemy;

    protected abstract void OnEnemyDetected(GameObject enemy, Team enemyTeam);
    protected abstract void OnEnemyLost(GameObject enemy, Team enemyTeam);

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<ITeamMember>();
        if (enemy == null) return;

        // Notify the child class about the detected enemy
        if (enemy.GetTeam() != GetTeam())
        {
            OnEnemyDetected(other.gameObject, enemy.GetTeam());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<ITeamMember>();
        if (enemy == null) return;

        // Notify the child class about the lost enemy
        if (enemy.GetTeam() != GetTeam())
        {
            OnEnemyLost(other.gameObject, enemy.GetTeam());
        }
    }

    // Abstract method for getting the team of the tower
    protected abstract Team GetTeam();
}
