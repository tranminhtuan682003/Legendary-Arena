using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Transform target;
    private LineRenderer lineRenderer;
    [SerializeField] private float widthLine;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = widthLine;
        lineRenderer.endWidth = widthLine;
    }

    void Update()
    {
        FireTarget();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FireTarget()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            lineRenderer.enabled = true;
            FocusToTarget();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }


    private void FocusToTarget()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);
        lineRenderer.material.color = Color.red;

        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;

        ObjectPool.Instance.SpawnFromPool("BulletTower", transform.position, transform.rotation, 0.5f);
    }

}
