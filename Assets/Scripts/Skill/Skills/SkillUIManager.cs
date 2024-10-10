using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUIManager : MonoBehaviour
{
    private HeroBase hero;
    [Header("nomal,skill1,skill2,skill3,goHome,heal,Explosive,Farming,Pushing")]
    public List<Button> skillButtons;
    public List<TextMeshProUGUI> cooldownText;
    public List<TextMeshProUGUI> nameSkill;
    public TextMeshProUGUI notice;

    // Màu sắc gốc và màu sắc khi đang cooldown
    private Color originalColor;
    private Color cooldownColor = Color.grey; // Thay đổi màu sắc khi cooldown

    private void Start()
    {
        hero = FindObjectOfType<HeroBase>();
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
        for (int i = 0; i < hero.currentHero.abilities.Count; i++)
        {
            if (i < skillButtons.Count && i < cooldownText.Count && i < nameSkill.Count)
            {
                int index = i;
                // Gán tên kỹ năng
                nameSkill[i].text = hero.currentHero.abilities[i].abilityName;

                // Thêm sự kiện khi nhấn nút
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => ActivateAbility(index));
            }
        }
    }

    private void ActivateAbility(int index)
    {
        var ability = hero.currentHero.abilities[index];

        // Kiểm tra nếu kỹ năng đang trong cooldown
        if (ability.IsOnCooldown)
        {
            StartCoroutine(ShowNotice());
            return;
        }

        // Kích hoạt kỹ năng
        hero.ActivateAbility(index);
        // Đổi màu nút
        ChangeButtonColor(skillButtons[index], cooldownColor);
    }

    // Cập nhật hiển thị thời gian hồi chiêu
    private void UpdateCooldowns()
    {
        for (int i = 0; i < hero.currentHero.abilities.Count; i++)
        {
            if (i < cooldownText.Count)
            {
                var ability = hero.currentHero.abilities[i];
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

    IEnumerator ShowNotice()
    {
        notice.text = "Đang hồi chiêu";
        yield return new WaitForSeconds(1f);
        notice.text = "";
    }

    private void ChangeButtonColor(Button button, Color color)
    {
        button.GetComponent<Image>().color = color;
    }
}
