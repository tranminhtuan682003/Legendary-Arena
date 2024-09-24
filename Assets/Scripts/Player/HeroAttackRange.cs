using System.Collections.Generic;
using UnityEngine;

public class HeroAttackRange : MonoBehaviour
{
    private HeroController heroController;
    private Transform target;
    private bool canFollowTarget;
    public List<LayerMask> layerTargets;

    void Awake()
    {
        heroController = GetComponentInParent<HeroController>();
    }

    void Update()
    {
        // if (canFollowTarget && target != null)
        // {
        //     heroController.FollowTargetBeforeAttack(canFollowTarget, target);
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer))
        {
            Debug.Log("can follow target");
            canFollowTarget = true;
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer))
        {
            canFollowTarget = false;
            target = null;
        }
    }

    bool IsInLayerMask(int layer)
    {
        foreach (var layerTarget in layerTargets)
        {
            if ((layerTarget.value & (1 << layer)) != 0)
            {
                return true;
            }
        }
        return false;
    }
}
