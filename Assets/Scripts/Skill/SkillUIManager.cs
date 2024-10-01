using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public PlayerController playerController;
    public List<Button> skillButtons;
    public List<TextMeshProUGUI> cooldownText;
    public List<TextMeshProUGUI> nameSkill;
    public TextMeshProUGUI notice;

    // Màu sắc gốc và màu sắc khi đang cooldown
    private Color originalColor;
    private Color cooldownColor = Color.grey; // Thay đổi màu sắc khi cooldown

    private void Start()
    {
        SetupSkillButtons();
        // Lưu màu sắc gốc của nút
        if (skillButtons.Count > 0)
        {
            originalColor = skillButtons[0].GetComponent<Image>().color; // Giả sử tất cả các nút có cùng màu
        }
    }

    private void Update()
    {
        UpdateCooldowns(); // Cập nhật cooldown mỗi frame
    }

    public void SetupSkillButtons()
    {
        for (int i = 0; i < playerController.currentHero.abilities.Count; i++)
        {
            if (i < skillButtons.Count && i < cooldownText.Count && i < nameSkill.Count)
            {
                int index = i;
                // Gán tên kỹ năng
                nameSkill[i].text = playerController.currentHero.abilities[i].abilityName;

                // Thêm sự kiện khi nhấn nút
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => ActivateAbility(index));
            }
        }
    }

    private void ActivateAbility(int index)
    {
        var ability = playerController.currentHero.abilities[index];

        // Kiểm tra nếu kỹ năng đang trong cooldown
        if (ability.IsOnCooldown)
        {
            notice.text = ability.abilityName + " is on cooldown!"; // Hiển thị thông báo
            return; // Không kích hoạt kỹ năng nếu đang cooldown
        }

        // Kích hoạt kỹ năng
        playerController.ActivateAbility(index);
        // Đổi màu nút
        ChangeButtonColor(skillButtons[index], cooldownColor);
    }

    // Cập nhật hiển thị thời gian hồi chiêu
    private void UpdateCooldowns()
    {
        for (int i = 0; i < playerController.currentHero.abilities.Count; i++)
        {
            if (i < cooldownText.Count)
            {
                var ability = playerController.currentHero.abilities[i];
                if (ability.IsOnCooldown)
                {
                    float cooldownRemaining = ability.cooldown - (Time.time - ability.LastUsedTime);
                    if (cooldownRemaining <= 1f)
                    {
                        cooldownText[i].text = cooldownRemaining.ToString("F1") + "s";  // Hiển thị số thập phân
                    }
                    else
                    {
                        cooldownText[i].text = Mathf.Floor(cooldownRemaining).ToString() + "s";  // Hiển thị số nguyên
                    }
                }
                else
                {
                    cooldownText[i].text = "";
                    ChangeButtonColor(skillButtons[i], originalColor);
                }
            }
        }
    }


    // Hàm thay đổi màu của nút
    private void ChangeButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}