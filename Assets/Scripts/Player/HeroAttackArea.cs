using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackArea : MonoBehaviour
{
    // Biến public
    private HeroController heroController;
    public LineRenderer lineRenderer;
    public int subdivision = 10;
    public float radius = 5f;
    public List<LayerMask> layerTarget;

    // Biến private

    private float scaleAreaAttack = 1f;
    private bool canAttack;
    private float timeUseSkill = 4f;
    private Transform target;
    public bool canFollowTarget;

    void Start()
    {
        heroController = GetComponentInParent<HeroController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        heroController.CheckAttack(canAttack, target);
        DrawAttackArea();

        if (heroController.changeScale)
        {
            ChangeScaleAreaAttack();
        }

        lineRenderer.enabled = true;
    }

    private void ChangeScaleAreaAttack()
    {
        scaleAreaAttack = 1.5f;
        transform.localScale = new Vector3(1, 1, 1) * scaleAreaAttack;
        heroController.changeScale = false;

        StartCoroutine(ReturnScaleNomal());
    }

    private IEnumerator ReturnScaleNomal()
    {
        yield return new WaitForSeconds(timeUseSkill);
        scaleAreaAttack = 1f;
        transform.localScale = new Vector3(1, 1, 1) * scaleAreaAttack;
    }

    private void DrawAttackArea()
    {
        float angleStep = 2f * Mathf.PI / subdivision;
        lineRenderer.positionCount = subdivision + 1;

        for (int i = 0; i <= subdivision; i++)
        {
            float angle = angleStep * i;
            float xPosition = radius * Mathf.Cos(angle);
            float zPosition = radius * Mathf.Sin(angle);

            Vector3 pointInCircle = new Vector3(xPosition, 0f, zPosition);
            lineRenderer.SetPosition(i, pointInCircle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && !canAttack)
        {
            canAttack = true;
            canFollowTarget = true;
            target = other.transform;
            lineRenderer.material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 && canAttack)
        {
            canAttack = false;
            target = null;
            lineRenderer.material.color = Color.red;
        }
    }

}
