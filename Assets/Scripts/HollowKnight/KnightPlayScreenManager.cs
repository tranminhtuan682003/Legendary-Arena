using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class KnightPlayScreenManager : MonoBehaviour
{
    private readonly string[] skillNames = { "Attack", "Skill1", "Skill2", "Skill3", "Heal", "Sup", "Recall" };
    private readonly string[] moveNames = { "Up", "Down", "Right", "Left" };

    private ButtonControlManager buttonKnightManager;
    private UIKnightManager uIKnightManager;
    private HealthBarKnightController HealthBarKnightController;

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
        SetupHealthBar(); // Thiết lập Health Bar
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
                // if (action != null)
                // {
                //     action.SetButtonKnightManager(buttonKnightManager);
                // }
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
                // if (moveAction != null)
                // {
                //     moveAction.SetButtonKnightManager(buttonKnightManager);
                // }
            }
        }
    }

    // Tìm và thiết lập Health Bar
    private void SetupHealthBar()
    {
        var healthBarSlider = FindHealthBar();
        if (healthBarSlider != null)
        {
            HealthBarKnightController = healthBarSlider.gameObject.GetComponent<HealthBarKnightController>();
            if (HealthBarKnightController == null)
            {
                HealthBarKnightController = healthBarSlider.gameObject.AddComponent<HealthBarKnightController>();
            }
        }
        else
        {
            Debug.LogWarning("Health Bar Slider not found!");
        }
    }

    // Tìm Slider health bar
    private Slider FindHealthBar()
    {
        var sliders = GetComponentsInChildren<Slider>(true); // Bao gồm cả các đối tượng Inactive
        foreach (var slider in sliders)
        {
            if (slider.name == "HealthBar") // Giả sử Slider health bar có tên là "HealthBar"
            {
                return slider;
            }
        }
        return null;
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
}
