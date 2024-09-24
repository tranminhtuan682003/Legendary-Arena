using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AttackAreaTower : MonoBehaviour
{
    private TowerController towerController;
    [SerializeField] private int segments = 50;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float lineWidth;
    public List<LayerMask> layerTargets;
    public Transform target;
    public bool canFollowTarget;

    private LineRenderer lineRenderer;

    void Start()
    {
        towerController = transform.parent.GetComponentInChildren<TowerController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.material.color = Color.green;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        CreatePoints();
    }

    void Update()
    {
        if (target != null)
        {
            towerController.SetTarget(target);
        }
    }


    void CreatePoints()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += (360f / segments);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer))
        {
            canFollowTarget = true;
            lineRenderer.material.color = Color.red;
            target = other.transform;
            Debug.Log("Target acquired: " + target.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer))
        {
            canFollowTarget = false;
            lineRenderer.material.color = Color.green;
            Debug.Log("Target lost: " + (target != null ? target.name : "None"));
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