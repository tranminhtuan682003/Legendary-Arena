using UnityEngine;

public class BulletTower : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            IhealthPlayer player = other.gameObject.GetComponent<IhealthPlayer>();

            if (player != null)
            {
                player.TakeDamage(1000f);
                gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("SoldierEnemy"))
        {
            IHealthSoldier soldierEnemy = other.gameObject.GetComponent<IHealthSoldier>();
            if (soldierEnemy != null)
            {
                soldierEnemy.TakeDamage(500f);
                gameObject.SetActive(false);
            }
        }
    }

}
