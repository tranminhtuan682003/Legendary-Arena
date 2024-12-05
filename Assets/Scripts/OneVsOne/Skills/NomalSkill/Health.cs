using UnityEngine;
using System.Collections;

public class Health : SkillBase
{
    private GameObject effect;
    private int extraHealth = 5;
    public override void Execute()
    {
        ActivateEffect();
        StartCoroutine(Healing()); // Bắt đầu quá trình hồi máu
    }

    private void ActivateEffect()
    {
        if (skillData.skillEffect != null)
        {
            StartCoroutine(ReturnEffectToPool());
        }
    }

    private IEnumerator ReturnEffectToPool()
    {
        effect = ObjectPool.Instance.GetFromPool(skillData.skillEffect, hero.transform.position, hero.transform.rotation);
        yield return new WaitForSeconds(5f);
        effect.SetActive(false);
    }

    private IEnumerator Healing()
    {
        int healCount = 5;
        for (int i = 0; i < healCount; i++)
        {
            Debug.Log("Healing");
            hero.Heal(extraHealth);
            yield return new WaitForSeconds(1f);
        }
    }
}
