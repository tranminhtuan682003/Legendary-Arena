using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillKnightManager : MonoBehaviour
{
    private Dictionary<string, Action> skillActions;
    private Button[] buttons; // Tất cả các Button trong giao diện

    private void Start()
    {
        InitializeSkills();
        AssignButtons();
    }

    private void InitializeSkills()
    {
        // Khởi tạo các hành động kỹ năng
        skillActions = new Dictionary<string, Action>
        {
            { "Attack", () => Attack() },
            { "Skill1", () => Skill1() },
            { "Skill2", () => Skill2() },
            { "Skill3", () => Skill3() },
            { "Supplymentary", () => Supplymentary() },
            { "Heal", () => Heal() },
            { "Recall", () => Recall() }
        };
    }

    private void AssignButtons()
    {
        // Lấy tất cả các Button trong giao diện
        buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            string nameSkill = button.name; // Dùng tên của Button làm key

            // Gán sự kiện click
            button.onClick.RemoveAllListeners(); // Đảm bảo không có sự kiện cũ
            button.onClick.AddListener(() =>
            {
                if (skillActions.TryGetValue(nameSkill, out var action))
                {
                    action.Invoke();
                }
                else
                {
                    Debug.LogWarning($"No action found for skill: {nameSkill}");
                }
            });
        }
    }

    // Các hành động kỹ năng
    private void Attack()
    {
        Debug.Log("Attack click");
    }
    private void Skill1()
    {
        Debug.Log("Skill1 click");
    }
    private void Skill2()
    {
        Debug.Log("Skill2 click");
    }
    private void Skill3()
    {
        Debug.Log("Skill3 click");
    }
    private void Supplymentary()
    {
        Debug.Log("Supplymentary click");
    }
    private void Heal()
    {
        Debug.Log("Heal click");
    }
    private void Recall()
    {
        Debug.Log("Recall click");
    }
}
