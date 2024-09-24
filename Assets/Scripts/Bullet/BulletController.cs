using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    public List<LayerMask> layerTargets;
    [SerializeField] private float speed = 100f;
    private AttackAreaTower attackAreaTower;

    void Awake()
    {
        attackAreaTower = FindObjectOfType<AttackAreaTower>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        StartCoroutine(DeActive());
    }

    void Update()
    {
        if (attackAreaTower.canFollowTarget && attackAreaTower.target != null)
        {
            FollowTarget();
        }
        else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        rb.velocity = transform.forward * speed;
    }

    private void FollowTarget()
    {
        Vector3 direction = (attackAreaTower.target.position - transform.position).normalized;
        transform.forward = direction;
        rb.velocity = direction * speed;
    }

    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (var layerTarget in layerTargets)
        {
            if (((1 << other.gameObject.layer) & layerTarget) != 0)
            {
                IHealth health = other.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(40);
                }
                gameObject.SetActive(false);
                break;
            }
        }
    }

}
