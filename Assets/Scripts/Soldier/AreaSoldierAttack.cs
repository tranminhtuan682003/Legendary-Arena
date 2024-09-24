using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSoldierAttack : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask towerEnemyLayer;
    public SoldierController soldierController;

    private List<Transform> targetsInRange = new List<Transform>();
    private Transform currentTarget;

    void Start()
    {
        // Initialize logic if necessary
    }

    void Update()
    {
        if (currentTarget != null)
        {
            soldierController.SetTarget(true, currentTarget);
        }
        else if (targetsInRange.Count > 0)
        {
            // Chọn mục tiêu mới từ danh sách khi không còn mục tiêu hiện tại
            currentTarget = targetsInRange[0];
            soldierController.SetTarget(true, currentTarget);
        }
        else
        {
            soldierController.SetTarget(false, null);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other.gameObject.layer))
        {
            targetsInRange.Add(other.transform);

            // Nếu không có mục tiêu nào đang theo dõi, chọn mục tiêu mới ngay lập tức
            if (currentTarget == null)
            {
                currentTarget = other.transform;
                soldierController.SetTarget(true, currentTarget);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (targetsInRange.Contains(other.transform))
        {
            targetsInRange.Remove(other.transform);

            if (currentTarget == other.transform)
            {
                currentTarget = null;

                // Chọn mục tiêu tiếp theo nếu có trong danh sách
                if (targetsInRange.Count > 0)
                {
                    currentTarget = targetsInRange[0];
                }
            }
        }
    }

    private bool IsEnemy(int layer)
    {
        return (enemyLayer == (enemyLayer | (1 << layer))) || (towerEnemyLayer == (towerEnemyLayer | (1 << layer)));
    }
}
