using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillCooldowns : MonoBehaviour
{
    private Dictionary<string, (bool isOnCooldown, TextMeshProUGUI cooldownText, float cooldownTime)> skillCooldowns;

    public void Initialize(List<SkillManager> skills)
    {
        skillCooldowns = new Dictionary<string, (bool, TextMeshProUGUI, float)>();

        foreach (var skill in skills)
        {
            skillCooldowns[skill.skillName] = (false, skill.cooldownText, skill.cooldownTime);
        }
    }

    public bool IsSkillOnCooldown(string skillName)
    {
        return skillCooldowns.TryGetValue(skillName, out var cooldownInfo) && cooldownInfo.isOnCooldown;
    }

    public void StartCooldown(string skillName)
    {
        if (skillCooldowns.TryGetValue(skillName, out var cooldownInfo))
        {
            if (!cooldownInfo.isOnCooldown)
            {
                StartCoroutine(CooldownCoroutine(skillName, cooldownInfo.cooldownText, cooldownInfo.cooldownTime));
            }
        }
    }

    private IEnumerator CooldownCoroutine(string skillName, TextMeshProUGUI textUI, float cooldownTime)
    {
        skillCooldowns[skillName] = (true, textUI, cooldownTime);

        float elapsedTime = 0f;
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = Mathf.Max(0f, cooldownTime - elapsedTime);
            textUI.text = remainingTime < 1f ? $"{remainingTime:F1}" : $"{remainingTime:F0}";
            yield return null;
        }

        textUI.text = "";
        skillCooldowns[skillName] = (false, textUI, cooldownTime);
    }
}
