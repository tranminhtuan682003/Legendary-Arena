using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
public class Skill1Valhein : SkillBase
{
    public override void Execute()
    {
        Debug.Log("da nhân");
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
        GameObject effect = ObjectPool.Instance.GetFromPool(skillData.skillEffect, hero.transform.position, hero.transform.rotation);
        yield return new WaitForSeconds(8f);
        effect.SetActive(false);
        hero.transform.position = new Vector3(-32, 0, -30.5f);
    }

    private void PlaySound()
    {
        if (skillData.soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(skillData.soundEffect, hero.transform.position);
        }
    }
}
