using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class SkillManager : MonoBehaviour
{
    private Dictionary<string, Button> skillButtons = new Dictionary<string, Button>();
    private HeroBase hero;
    private SkillConfig currentSkillConfig;

    private void OnEnable()
    {
        HeroEventManager.OnHeroCreated += OnHeroCreated;
        InitializeButtons();
    }

    private void OnDisable()
    {
        HeroEventManager.OnHeroCreated -= OnHeroCreated;
    }

    private void InitializeButtons()
    {
        for (int i = 1; i <= 8; i++)
        {
            skillButtons[$"Skill{i}"] = GameObject.Find($"Skill{i}")?.GetComponent<Button>();
        }
        skillButtons["ExtraSkill"] = GameObject.Find("ExtraSkill")?.GetComponent<Button>();
    }

    private void OnHeroCreated(HeroBase hero)
    {
        this.hero = hero;
        string address = $"SkillConfig_{hero.name}"; // Đặt tên theo địa chỉ đã gán cho Addressables
        Addressables.LoadAssetAsync<SkillConfig>(address).Completed += OnSkillConfigLoaded;
    }

    private void OnSkillConfigLoaded(AsyncOperationHandle<SkillConfig> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentSkillConfig = handle.Result;
            AssignSkills();
        }
    }

    private void AssignSkills()
    {
        if (currentSkillConfig == null) return;
        for (int i = 0; i < currentSkillConfig.defaultSkills.Length && i < 8; i++)
        {
            var button = skillButtons[$"Skill{i + 1}"];
            CreateAndAssignSkill(currentSkillConfig.defaultSkills[i], button);
        }
        if (currentSkillConfig.extraSkill != null)
        {
            var extraSkillButton = skillButtons["ExtraSkill"];
            if (UIManager.Instance.extraSkillName == "")
            {
                currentSkillConfig.extraSkill.skillName = "Execute";
            }
            else
            {
                currentSkillConfig.extraSkill.skillName = UIManager.Instance.extraSkillName;
                CreateAndAssignSkill(currentSkillConfig.extraSkill, extraSkillButton);
            }
        }
    }

    private void CreateAndAssignSkill(SkillConfig.SkillData skillData, Button button)
    {
        ISkill skill = SkillFactory.CreateSkill(skillData, hero);
        if (skill != null)
        {
            (skill as SkillBase)?.SetData(skillData, hero);
            button.onClick.AddListener(() => skill.Execute());
        }
    }
}
