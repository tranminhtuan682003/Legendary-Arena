using UnityEngine;
using System.Collections;
public abstract class SkillBase : MonoBehaviour, ISkill
{
    public SkillConfig.SkillData skillData;
    protected HeroBase hero;

    public void SetHero(HeroBase hero) => this.hero = hero;

    public virtual void Execute()
    {
        PlaySound();
        ActivateEffect();
    }

    public void Initialize(SkillConfig.SkillData data, HeroBase hero)
    {
        skillData = data;
        this.hero = hero;
    }

    private void ActivateEffect()
    {
        if (skillData.skillEffect != null)
        {
            GameObject effect = ObjectPool.Instance.GetFromPool(skillData.skillEffect, hero.transform.position, hero.transform.rotation);
            effect.SetActive(true);
            StartCoroutine(ReturnEffectToPool(effect, 2f));
        }
    }

    private void PlaySound()
    {
        if (skillData.soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(skillData.soundEffect, hero.transform.position);
        }
    }

    private IEnumerator ReturnEffectToPool(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }
}