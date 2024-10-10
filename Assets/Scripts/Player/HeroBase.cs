using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public abstract class HeroBase : MonoBehaviour, IhealthPlayer
{
    public HeroParameter heroParameter;
    public Heros currentHero;
    private IState currentState;
    public HeroEffects heroEffects;
    public SupplementaryTable supplementaryTable;
    public StartPosition startPosition;
    private AttackArea attackArea;
    SkillUIManager skillUIManager;

    #region IHealthPlayer
    public float CurrentHealth
    {
        get => heroParameter.currentHealth;
        set => heroParameter.currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth
    {
        get => heroParameter.maxHealth;
        set => heroParameter.maxHealth = Mathf.Max(0, value);
    }

    public bool IsDead => CurrentHealth <= 0;

    public void Heal(float amount)
    {
        if (IsDead) return;
        CurrentHealth += amount;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("Hero is Dead!");
    }
    #endregion

    protected virtual void Awake()
    {
        InitComponent();
    }

    protected virtual void Start()
    {
        InitParameter();
        InitPlayer();
        InitializeEffects();
    }

    protected virtual void Update()
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
        attackArea = GetComponentInChildren<AttackArea>();
        currentHero = new Marksman(this);
    }

    protected virtual void InitParameter()
    {
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
        skillUIManager = FindObjectOfType<SkillUIManager>();
        // skillUIManager.SetupSkillButtons();
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

    public void ActivateAbility(int abilityIndex)
    {
        if (abilityIndex < currentHero.abilities.Count)
        {
            currentHero.abilities[abilityIndex].Activate();
        }
    }

    public virtual void Attack()
    {
        ChangeAnimator("Attack");
    }

    protected IEnumerator ShowAttackArea()
    {
        ActivateLightEffect();
        yield return new WaitForSeconds(0.2f);
        DeactivateLightEffect();
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

    public void AdjustSpeedShoot(float attackSpeed)
    {
        heroParameter.attackSpeed = attackSpeed;
    }

    #region Movement Methods
    protected void OnMove(InputValue value)
    {
        heroParameter.moveDirection = value.Get<Vector2>().normalized;
        heroParameter.movementVector = new Vector3(heroParameter.moveDirection.x, 0, heroParameter.moveDirection.y);
    }

    public void Move()
    {
        if (heroParameter.movementVector != Vector3.zero && !IsDead)
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
