using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class KnightPlayScreenManager : MonoBehaviour, IScreenKnightManager
{
    private ButtonPlayKnightManager buttonKnightManager;
    private List<Button> skillButtons = new List<Button>();

    [Inject]
    public void Construct(ButtonPlayKnightManager buttonKnightManager)
    {
        this.buttonKnightManager = buttonKnightManager;
        SetupMoveButtons();
        SetupSkillButtons();
    }

    // Setup các nút di chuyển
    private void SetupMoveButtons()
    {
        var moveButtons = GetComponentsInChildren<MoveButtonAction>(true); // Tìm cả nút ẩn
        foreach (var button in moveButtons)
        {
            button.SetButtonKnightManager(buttonKnightManager);
        }
    }

    // Tìm và cài đặt các nút kỹ năng
    private void SetupSkillButtons()
    {
        // Tạo danh sách các kỹ năng và class tương ứng
        var skillMappings = new Dictionary<string, Type>
        {
            { "Attack", typeof(KnightAttack) },
            { "Skill1", typeof(KnightSkill1) },
            { "Skill2", typeof(KnightSkill2) },
            { "Skill3", typeof(KnightSkill3) },
            { "Heal", typeof(KnightHeal) },
            { "Sup", typeof(KnightSup) },
            { "Recall", typeof(KnightRecall) }
        };

        foreach (var skill in skillMappings)
        {
            var buttonTransform = transform.Find(skill.Key);
            if (buttonTransform != null)
            {
                var button = buttonTransform.GetComponent<Button>();
                if (button != null)
                {
                    // Gắn ButtonKnightManager cho AttackButtonAction
                    var attackAction = button.GetComponent<AttackButtonAction>();
                    if (attackAction == null)
                    {
                        attackAction = button.gameObject.AddComponent<AttackButtonAction>();
                    }
                    attackAction.SetButtonKnightManager(buttonKnightManager);

                    // Gắn class xử lý kỹ năng (KnightSkillX)
                    if (!button.gameObject.TryGetComponent(skill.Value, out _))
                    {
                        button.gameObject.AddComponent(skill.Value); // Thêm script class nếu chưa có
                    }

                    skillButtons.Add(button);
                }
                else
                {
                    Debug.LogWarning($"Button component missing on '{skill.Key}' object.");
                }
            }
            else
            {
                Debug.LogWarning($"Button '{skill.Key}' not found in the hierarchy.");
            }
        }
    }
}
