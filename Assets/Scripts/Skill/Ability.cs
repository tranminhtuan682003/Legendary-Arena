using UnityEngine;

public abstract class Ability
{
    public string abilityName;
    public float cooldown;
    public float manaCost;
    public ParticleSystem effectSkill;
    private float lastUsedTime = -1f;
    public float LastUsedTime => lastUsedTime;
    public bool IsOnCooldown
    {
        get
        {
            return lastUsedTime >= 0 && Time.time - lastUsedTime < cooldown;
        }
    }

    public virtual void Activate()
    {
        if (!IsOnCooldown)
        {
            lastUsedTime = Time.time;
            UseAbility();
        }
        else
        {
            float cooldownRemaining = cooldown - (Time.time - lastUsedTime);
            Debug.Log(abilityName + " is on cooldown for " + cooldownRemaining.ToString("F1") + " seconds.");
        }
    }

    protected abstract void UseAbility();
}
