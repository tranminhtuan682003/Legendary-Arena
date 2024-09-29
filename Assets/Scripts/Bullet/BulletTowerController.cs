using UnityEngine;

public class BulletTowerController : MonoBehaviour
{
    private float speed = 20f;
    private System.Action onDeactivate;
    private Transform target;

    public void Fire(Transform target, System.Action onDeactivate)
    {
        this.target = target;
        this.onDeactivate = onDeactivate;
    }

    private void Update()
    {
        if (target != null)
        {
            // Di chuyển đạn về phía mục tiêu
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Kiểm tra khoảng cách đến mục tiêu
            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                onDeactivate?.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IHealthEnemy enemyHealth = other.gameObject.GetComponent<IHealthEnemy>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1000f);
            }
        }
        else if (other.gameObject.CompareTag("SoldierEnemy"))
        {
            IHealthSoldier soldierEnemy = other.gameObject.GetComponent<IHealthSoldier>();
            if (soldierEnemy != null)
            {
                soldierEnemy.TakeDamage(500f);
            }
        }
    }

}
