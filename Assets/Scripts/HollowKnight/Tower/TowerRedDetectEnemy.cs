using UnityEngine;

public class TowerRedDetectEnemy : MonoBehaviour
{
    private TowerRedController towerController;

    private void Awake()
    {
        // Lấy tham chiếu đến TowerRedController của trụ hiện tại
        towerController = GetComponentInParent<TowerRedController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<ITeamMember>();
        if (enemy == null) return;

        if (enemy.GetTeam() == Team.Blue)
        {
            // Thông báo trực tiếp đến TowerRedController
            towerController.HandleDetectEnemy(other.gameObject, Team.Blue);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<ITeamMember>();
        if (enemy == null) return;

        if (enemy.GetTeam() == Team.Blue)
        {
            // Thông báo kẻ địch đã rời khỏi phạm vi
            towerController.HandleDetectEnemy(null, Team.None);
        }
    }
}
