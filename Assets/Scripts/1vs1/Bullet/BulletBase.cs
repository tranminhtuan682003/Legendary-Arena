using System.Collections;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    private Rigidbody rb;
    private float speedMove;
    private int damage;
    private float attackRange;
    private float timeLife;
    protected Transform target;

    protected virtual void OnEnable() { }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleMove();
    }

    public void Initialize(float speedMove, int damage, float attackRange)
    {
        this.speedMove = speedMove;
        this.damage = damage;
        this.attackRange = attackRange;

        timeLife = attackRange / speedMove;
    }

    protected virtual void OnTargetDetected(Transform newTarget)
    {
        target = newTarget;
    }

    private void HandleMove()
    {
        if (target != null && target.gameObject.activeInHierarchy) // Kiểm tra nếu target vẫn active
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
            Vector3 newPosition = transform.position + direction * speedMove * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            target = null; // Đặt target về null nếu không còn active
            rb.velocity = transform.forward * speedMove;
            StartCoroutine(DisableAfterTime(timeLife));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.activeInHierarchy) return; // Kiểm tra nếu đối tượng vẫn còn hoạt động

        ITeamMember targetMember = other.GetComponent<ITeamMember>();

        if (targetMember != null && other.transform == target)
        {
            ApplyDamage(other);
            gameObject.SetActive(false);
        }
    }


    private void ApplyDamage(Collider other)
    {
        if (other.TryGetComponent<HeroBase>(out var player))
        {
            player.TakeDamage(damage);
        }
        else if (other.TryGetComponent<SoldierBase>(out var soldier))
        {
            soldier.TakeDamage(damage);
        }
        else if (other.TryGetComponent<TurretBase>(out var turret))
        {
            turret.TakeDamage(damage);
        }
    }

    // Coroutine để vô hiệu hóa viên đạn sau khi hết thời gian tồn tại
    private IEnumerator DisableAfterTime(float timeLife)
    {
        yield return new WaitForSeconds(timeLife);
        gameObject.SetActive(false);
    }
}
