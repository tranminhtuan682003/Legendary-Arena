using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class HeroBase : MonoBehaviour, IhealthPlayer
{
    public HeroParameter heroParameter;
    private IState currentState;
    public HeroEffects heroEffects;
    private const string HeroEffectAddress = "Assets/Scripts/Skill/ScriptTableObject/TelAnas.asset";
    public StartPosition startPosition;
    private AttackArea attackArea;
    private GameObject healthBar;
    private bool healthBarReady;

    #region IHealthPlayer Implementation

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

    #region Initialization Methods

    protected virtual void Awake()
    {
        HeroEventManager.TriggerHeroCreated(this);
        InitComponent();
    }

    protected virtual void Start()
    {
        InitializeEffects();
    }

    protected void InitComponent()
    {
        heroParameter.animator = GetComponentInChildren<Animator>();
        heroParameter.rigidbody = GetComponent<Rigidbody>();
        attackArea = GetComponentInChildren<AttackArea>();
    }

    protected virtual void InitParameter()
    {
        heroParameter.timeAttackAnimation = GetTimeAnimation("Attack", heroParameter.animator);
    }

    protected void InitializeEffects()
    {
        Addressables.LoadAssetAsync<HeroEffects>(HeroEffectAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                heroEffects = handle.Result;
                InitializeSkillEffects();
                InitHealthBar();
                InitPlayer();
                InitParameter();
            }
            else
            {
                Debug.LogError("Failed to load HeroEffects: " + handle.OperationException);
            }
        };
    }

    private void InitializeSkillEffects()
    {
        heroParameter.skillEffect = new Dictionary<string, ParticleSystem>
        {
            { "shootEffect", Instantiate(heroEffects.shootEffect) },
            { "returnHomeEffect", Instantiate(heroEffects.returnHomeEffect) },
            { "skill1", Instantiate(heroEffects.Skill1) },
            { "skill2", Instantiate(heroEffects.Skill2) },
            { "skill3", Instantiate(heroEffects.Skill3) },
        };

        foreach (var effect in heroParameter.skillEffect.Values)
        {
            effect.Stop();
        }
    }

    protected virtual void InitPlayer()
    {
        ChangeState(new PlayerIdleState(this));
        ObjectPool.Instance.CreatePool(heroEffects.bulletPrefab);
        heroParameter.timeAttackAnimation = GetTimeAnimation("Attack", heroParameter.animator);
    }

    private void InitHealthBar()
    {
        healthBar = Instantiate(heroEffects.healthBar);
        healthBarReady = true;
    }

    private void SetPositionHealthBar()
    {
        healthBar.transform.position = transform.position + new Vector3(0, 2.25f, 0);
    }

    #endregion

    #region Update and State Management

    protected virtual void Update()
    {
        currentState?.Execute();
    }

    protected virtual void LateUpdate()
    {
        if (healthBarReady)
        {
            SetPositionHealthBar();
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    #endregion

    #region Animation and Effects

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

    public void ActivateLightEffect()
    {
        attackArea.EnableLine();
    }

    public void DeactivateLightEffect()
    {
        attackArea.DisableLine();
    }

    #endregion

    #region Combat and Attack

    public virtual void Attack()
    {
        ChangeAnimator("Attack");
    }

    public IEnumerator Shooting()
    {
        heroParameter.isAttacking = true;
        yield return new WaitForSeconds(heroParameter.attackSpeed);
        heroParameter.isAttacking = false;
    }

    protected IEnumerator ShowAttackArea()
    {
        ActivateLightEffect();
        yield return new WaitForSeconds(0.2f);
        DeactivateLightEffect();
    }

    public void AdjustSpeedShoot(float attackSpeed)
    {
        heroParameter.attackSpeed = attackSpeed;
    }

    #endregion

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

    #region Utility Methods

    public void SetMaxRange(float maxRange)
    {
        heroParameter.attackRange = maxRange;
        attackArea.SetRadius(heroParameter.attackRange * 10);
    }

    protected virtual void OnDestroy()
    {
        Debug.Log("HeroBase đã bị hủy.");
    }

    #endregion
}
