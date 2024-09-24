using System.Collections.Generic;
using UnityEngine;

public class AreaMonsterAttack : MonoBehaviour
{
    public List<LayerMask> layerTarget;
    public MonsterController monsterController;
    private bool isFollowing;
    private Transform target;

    void Update()
    {
        if (isFollowing && target != null)
        {
            monsterController.SetTarget(isFollowing, target);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (target == null) // Chỉ theo dõi nếu chưa có mục tiêu
        {
            foreach (var layer in layerTarget)
            {
                if ((1 << other.gameObject.layer) == layer.value)
                {
                    isFollowing = true;
                    target = other.transform;
                    break; // Dừng vòng lặp khi tìm thấy mục tiêu
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (target == other.transform) // Chỉ ngừng theo dõi nếu mục tiêu hiện tại rời đi
        {
            isFollowing = false;
            target = null;
        }
    }
}
