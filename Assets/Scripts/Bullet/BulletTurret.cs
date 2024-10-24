using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    private float speed = 25f;
    private Transform target;
    void Start()
    {
        target = GamePlay1vs1Manager.Instance.target;
    }

    private void Update()
    {
        Vector3 direction = (target.position + new Vector3(0, 1, 0) - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
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
