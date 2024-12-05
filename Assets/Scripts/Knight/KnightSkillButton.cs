using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightSkillButton : MonoBehaviour
{
    private string nameSkill;
    private Button button;
    private Dictionary<string, Action> skillActions;

    private void OnEnable()
    {
        InỉtLize();
    }

    private void InỉtLize()
    {
        button = GetComponent<Button>();
        nameSkill = button.name;

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
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        if (skillActions.TryGetValue(nameSkill, out var action))
        {
            action.Invoke();
        }
    }

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
