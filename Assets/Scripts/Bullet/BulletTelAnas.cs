using System.Collections;
using UnityEngine;

public class BulletTelAnas : MonoBehaviour
{
    private float speed = 15f;
    public float range = 2.5f;
    private Rigidbody rb;

    void Awake()
    {
        // Chỉ khởi tạo Rigidbody một lần
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Đặt vận tốc khi đạn được kích hoạt
        rb.velocity = transform.forward * speed;

        // Tính toán thời gian bay dựa trên range và bắt đầu Coroutine
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
        // Đặt lại tầm bắn mới
        this.range = maxRange;
    }
}
