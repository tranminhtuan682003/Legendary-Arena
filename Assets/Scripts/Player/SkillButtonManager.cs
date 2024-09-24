using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Thư viện cho EventTrigger
using System.Collections; // Thư viện cho IEnumerator
using System.Collections.Generic; // Thư viện cho List


public class SkillButtonManager : MonoBehaviour
{
    [Header("Skill Buttons")]
    [SerializeField] private List<Button> skillButtons;

    [Header("Skill Names")]
    [SerializeField] private List<string> skillNames;

    private HeroController heroController;
    private SkillCooldowns cooldownManager;

    private void Start()
    {
        heroController = FindObjectOfType<HeroController>();
        cooldownManager = heroController.GetComponent<SkillCooldowns>();

        for (int i = 0; i < skillButtons.Count; i++)
        {
            string skillName = skillNames[i];
            Button button = skillButtons[i];
            AddButtonListeners(button, skillName);
        }
    }

    private void Update()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            string skillName = skillNames[i];
            Button button = skillButtons[i];
            button.interactable = !cooldownManager.IsSkillOnCooldown(skillName);
        }
    }

    private void AddButtonListeners(Button button, string skillName)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Thêm sự kiện OnPointerDown để bắt đầu tấn công
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { OnSkillButtonClick(skillName); });
        trigger.triggers.Add(pointerDownEntry);

        // Thêm sự kiện OnPointerUp để ngừng tấn công
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { OnSkillButtonRelease(); });
        trigger.triggers.Add(pointerUpEntry);
    }

    private void OnSkillButtonClick(string skillName)
    {
        heroController.attacking = true;
        heroController.ChangeState(new AttackState(heroController));
        heroController.OnAttack(skillName);
    }

    private void OnSkillButtonRelease()
    {
        heroController.attacking = false;
    }
}
