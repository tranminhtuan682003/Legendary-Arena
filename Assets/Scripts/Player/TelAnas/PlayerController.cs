using UnityEngine;

public class PlayerController : HeroBase
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void InitParameter()
    {
        base.InitParameter();
        heroParameter.maxHealth = 4100f;
        heroParameter.currentHealth = heroParameter.maxHealth;
        heroParameter.speed = 10f;
        heroParameter.attackSpeed = heroParameter.timeAttackAnimation;
        heroParameter.attackRange = 2.5f;
        heroParameter.spawnPoint = transform.Find("SpawnPoint");
    }

    protected override void InitPlayer()
    {
        base.InitPlayer();
    }

    public override void Attack()
    {
        base.Attack();
        StartCoroutine(Shooting());
        ActivateEffect("shootEffect", heroParameter.spawnPoint, 0.5f);
        GameObject bullet = ObjectPool.Instance.GetFromPool(heroDatabase.bulletPrefab, heroParameter.spawnPoint.position, heroParameter.spawnPoint.rotation);
        bullet.GetComponent<BulletTelAnas>().SetMaxRange(heroParameter.attackRange);
        if (heroParameter.attackRange == 2.5f)
        {
            StartCoroutine(ShowAttackArea());
        }
    }
}
