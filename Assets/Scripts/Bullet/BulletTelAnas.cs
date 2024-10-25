using System.Collections;
using UnityEngine;

public class BulletTelAnas : MonoBehaviour
{
    private float speed = 15f;
    public float range = 2.5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        float flightTime = range / speed;
        StartCoroutine(DisableAfterTime(flightTime));
    }

    private IEnumerator DisableAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    public void SetMaxRange(float maxRange)
    {
        range = maxRange;
    }
}
