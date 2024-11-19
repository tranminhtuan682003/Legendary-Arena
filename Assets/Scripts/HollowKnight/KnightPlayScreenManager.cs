using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class KnightPlayScreenManager : MonoBehaviour
{
    private readonly string[] skillNames = { "Attack", "Skill1", "Skill2", "Skill3", "Heal", "Sup", "Recall" };
    private readonly string[] moveNames = { "Up", "Down", "Right", "Left" };
    private HealthBarKnightController healthBar;
    private ButtonControlManager buttonKnightManager;
    private UIKnightManager uIKnightManager;

    [Inject]
    public void Construct(ButtonControlManager buttonKnightManager, UIKnightManager uIKnightManager)
    {
        this.buttonKnightManager = buttonKnightManager;
        this.uIKnightManager = uIKnightManager;
    }
    private void Awake()
    {
        SetupSkillButtons();
        SetupMoveButtons();
        FindHealthBar();
    }

    // Thiết lập các button skill
    private void SetupSkillButtons()
    {
        foreach (string buttonName in skillNames)
        {
            var button = FindButton(buttonName);
            if (button == null)
            {
                continue;
            }

            // Gán script tương ứng dựa trên tên button
            Type actionType = GetSkillActionType(buttonName);
            if (actionType != null)
            {
                var action = button.gameObject.AddComponent(actionType) as SkillKnightBase;
                if (action != null)
                {
                    action.SetButtonKnightManager(buttonKnightManager);
                }
            }
        }
    }

    // Thiết lập các nút di chuyển
    private void SetupMoveButtons()
    {
        foreach (string buttonName in moveNames)
        {
            var button = FindButton(buttonName);
            if (button == null)
            {
                continue;
            }

            // Gán script tương ứng dựa trên tên button
            Type moveType = GetMoveActionType(buttonName);
            if (moveType != null)
            {
                var moveAction = button.gameObject.AddComponent(moveType) as MoveKnightBase;
                if (moveAction != null)
                {
                    moveAction.SetButtonKnightManager(buttonKnightManager);
                }
            }
        }
    }

    // Tìm button theo tên (bao gồm cả button Inactive)
    private Button FindButton(string name)
    {
        var buttons = GetComponentsInChildren<Button>(true);
        foreach (var button in buttons)
        {
            if (button.name == name)
            {
                return button;
            }
        }
        return null;
    }

    // Lấy script hành động tương ứng với tên skill button
    private Type GetSkillActionType(string buttonName)
    {
        return buttonName switch
        {
            "Attack" => typeof(KnightAttack),
            "Skill1" => typeof(KnightSkill1),
            "Skill2" => typeof(KnightSkill2),
            "Skill3" => typeof(KnightSkill3),
            "Heal" => typeof(KnightHeal),
            "Sup" => typeof(KnightSup),
            "Recall" => typeof(KnightRecall),
            _ => null
        };
    }

    // Lấy script hành động tương ứng với tên move button
    private Type GetMoveActionType(string buttonName)
    {
        return buttonName switch
        {
            "Up" => typeof(KnightMoveUp),
            "Down" => typeof(KnightMoveDown),
            "Right" => typeof(KnightMoveRight),
            "Left" => typeof(KnightMoveLeft),
            _ => null
        };
    }

    private void FindHealthBar()
    {
        healthBar = GetComponentInChildren<HealthBarKnightController>(true);
    }

    public void SetMaxHealthBar(int maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); // Gọi phương thức từ HealthBar script
        }
        else
        {
            Debug.LogError("HealthBar reference is missing!");
        }
    }

    public void SetValueHealthBar(int health)
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(health); // Gọi phương thức từ HealthBar script
        }
        else
        {
            Debug.LogError("HealthBar reference is missing!");
        }
    }

}
