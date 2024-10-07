using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class HeroBase : MonoBehaviour, IhealthPlayer
{
    private HeroParameter heroParameter;
    public Hero currentHero;
    private IState currentState;
    public HeroEffects heroEffects;
    public SupplementaryTable supplementaryTable;
    private AttackArea attackArea;
    SkillUIManager skillUIManager;

    #region IHealthPlayer
    public float CurrentHealth { get => heroParameter.currentHealth; set => heroParameter.currentHealth = Mathf.Clamp(value, 0, MaxHealth); }
    public float MaxHealth { get => heroParameter.maxHealth; set => heroParameter.maxHealth = Mathf.Max(0, value); }

    public bool IsDead => throw new System.NotImplementedException();

    public void Heal(float amount)
    {
        if (heroParameter.isDead) return;
        CurrentHealth += amount;
    }

    public void TakeDamage(float amount)
    {
        if (heroParameter.isDead) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        heroParameter.isDead = true;
    }
    #endregion

    void Awake()
    {
        InitComponent();
    }

    void Start()
    {
        InitParameter();
    }

    void Update()
    {
        currentState?.Execute();
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected void InitComponent()
    {
        heroParameter.animator = GetComponentInChildren<Animator>();
        heroParameter.rigidbody = GetComponent<Rigidbody>();
        currentHero = new Marksman(this);
    }

    protected virtual void InitParameter()
    {
        heroParameter.maxHealth = 4000f;
        heroParameter.currentHealth = heroParameter.maxHealth;
        heroParameter.speed = 5f;
        heroParameter.timeAttackAnimation = GetTimeAnimation("Attack", heroParameter.animator);
    }

    protected void InitializeEffects()
    {
        if (heroEffects != null)
        {
            heroParameter.skillEffect = new Dictionary<string, ParticleSystem>
            {
                { "shootEffect", Instantiate(heroEffects.shootEffect) },
                { "returnHomeEffect", Instantiate(heroEffects.returnHomeEffect) },
                { "skill1", Instantiate(heroEffects.Skill1) },
                { "skill2", Instantiate(heroEffects.Skill2) },
                { "skill3", Instantiate(heroEffects.Skill3) }
            };
            foreach (var effect in heroParameter.skillEffect.Values)
            {
                effect.Stop();
            }
        }
    }

    protected virtual void InitPlayer()
    {
        ChangeState(new PlayerIdleState(this));
        InitSupplementary("Explosive");
        ObjectPool.Instance.CreatePool(heroEffects.bulletPrefab);
        heroParameter.timeAttackAnimation = GetTimeAnimation("Attack", heroParameter.animator);
        heroParameter.attackSpeed = heroParameter.timeAttackAnimation;
        skillUIManager = FindObjectOfType<SkillUIManager>();
        skillUIManager.SetupSkillButtons();
        heroParameter.attackRange = 2.5f; // khoi tao o player
    }

    protected void InitSupplementary(string name)
    {
        foreach (var item in supplementaryTable.supplementarys)
        {
            if (item.name == name)
            {
                heroParameter.supplymentary = Instantiate(item, transform);
                heroParameter.supplymentary.SetActive(false);
            }
        }
    }

    protected float GetTimeAnimation(string animationName, Animator animator)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return -1f;
    }

    protected void SetAnimationSpeed(string animationName, float speed)
    {
        AnimatorStateInfo stateInfo = heroParameter.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName))
        {
            heroParameter.animator.speed = speed;
        }
    }

    public void ChangeAnimator(string animationName)
    {
        heroParameter.animator.ResetTrigger("Idle");
        heroParameter.animator.ResetTrigger("Run");
        heroParameter.animator.ResetTrigger("Attack");
        heroParameter.animator.ResetTrigger("Dead");
        heroParameter.animator.SetTrigger(animationName);
    }

    public void SetMaxRange(float maxRange)
    {
        heroParameter.attackRange = maxRange;
        attackArea.SetRadius(heroParameter.attackRange * 10);
    }

    public void ActivateLightEffect()
    {
        attackArea.EnableLine();
    }

    public void DeactivateLightEffect()
    {
        attackArea.DisableLine();
    }

    protected IEnumerator ShowAttackArea()
    {
        ActivateLightEffect();
        yield return new WaitForSeconds(0.2f);
        DeactivateLightEffect();
    }

    public void ActivateAbility(int abilityIndex)
    {
        if (abilityIndex < currentHero.abilities.Count)
        {
            currentHero.abilities[abilityIndex].Activate();
        }
    }
    public void Attack()
    {
        StartCoroutine(Shooting());
        ChangeAnimator("Attack");
        if (heroEffects.rangeAttack == 2.5f)
        {
            StartCoroutine(ShowAttackArea());
        }
    }
    public IEnumerator Shooting()
    {
        heroParameter.isAttacking = true;
        yield return new WaitForSeconds(heroParameter.attackSpeed);
        heroParameter.isAttacking = false;
    }

    public void ActivateEffect(string effectName, Transform position, float duration)
    {
        if (heroParameter.skillEffect.ContainsKey(effectName))
        {
            ParticleSystem effect = heroParameter.skillEffect[effectName];
            effect.transform.position = position.position;
            effect.Play();
            StartCoroutine(DeactivateEffect(effect, duration));
        }
    }

    private IEnumerator DeactivateEffect(ParticleSystem effect, float duration)
    {
        yield return new WaitForSeconds(duration);
        effect.Stop();
    }

    #region Movement Methods
    protected void OnMove(InputValue value)
    {
        heroParameter.moveDirection = value.Get<Vector2>().normalized;
        heroParameter.movementVector = new Vector3(heroParameter.moveDirection.x, 0, heroParameter.moveDirection.y);
    }

    public void Move()
    {
        if (heroParameter.movementVector != Vector3.zero && !heroParameter.isDead)
        {
            heroParameter.isMoving = true;
            Vector3 move = heroParameter.movementVector * heroParameter.speed * Time.deltaTime;
            heroParameter.rigidbody.MovePosition(transform.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(heroParameter.movementVector);
            heroParameter.rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f));
        }
        else
        {
            heroParameter.isMoving = false;
        }
    }
    #endregion

}