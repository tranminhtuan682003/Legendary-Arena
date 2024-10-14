using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private Button returnHome;
    private Button health;
    private Button supplymentary;
    private Button nomalAttack;
    private Button killSoldier;
    private Button pushPillar;
    private Button skillButton1;
    private Button skillButton2;
    private Button skillButton3;

    private HeroBase hero;

    // Dictionary ánh xạ tên hero với các kỹ năng tương ứng
    private Dictionary<string, Type[]> heroSkills = new Dictionary<string, Type[]>
    {
        { "TelAnas", new Type[] { typeof(Return), typeof(Health),typeof(FlashButton), typeof(NomalAttack), typeof(KillSoldier), typeof(PushPillar), typeof(Skill1TelAnas), typeof(Skill2TelAnas), typeof(Skill3TelAnas) } },
        // Thêm các hero khác vào đây nếu cần
    };

    private void OnEnable()
    {
        HeroEventManager.OnHeroCreated += OnHeroCreated; // Đăng ký lắng nghe sự kiện khi hero được tạo
    }

    private void OnDisable()
    {
        HeroEventManager.OnHeroCreated -= OnHeroCreated; // Hủy đăng ký khi không cần lắng nghe
    }

    private void Start()
    {
        returnHome = GameObject.Find("Return")?.GetComponent<Button>();
        health = GameObject.Find("Health")?.GetComponent<Button>();
        supplymentary = GameObject.Find("Supplymentary")?.GetComponent<Button>();
        nomalAttack = GameObject.Find("NomalAttack")?.GetComponent<Button>();
        killSoldier = GameObject.Find("KillSoldier")?.GetComponent<Button>();
        pushPillar = GameObject.Find("PushPillar")?.GetComponent<Button>();
        skillButton1 = GameObject.Find("Skill1")?.GetComponent<Button>();
        skillButton2 = GameObject.Find("Skill2")?.GetComponent<Button>();
        skillButton3 = GameObject.Find("Skill3")?.GetComponent<Button>();

        if (returnHome == null || health == null || supplymentary == null || skillButton1 == null || skillButton2 == null || skillButton3 == null)
        {
            Debug.LogError("Một hoặc nhiều Button không tìm thấy trong Scene.");
            return; // Thoát để tránh lỗi trong các bước tiếp theo
        }
    }


    private void OnHeroCreated(HeroBase hero)
    {
        this.hero = hero;
        string heroName = hero.name.Replace("(Clone)", "").Trim();
        Debug.Log("Processed hero name: " + heroName);

        // Kiểm tra xem tên hero có trong Dictionary không
        if (heroSkills.ContainsKey(heroName))
        {
            Type[] skills = heroSkills[heroName];

            // Gán các kỹ năng liên quan đến SkillBase
            AssignSkillToButton(returnHome, skills[0], typeof(SkillBase));
            AssignSkillToButton(health, skills[1], typeof(SkillBase));
            AssignSkillToButton(supplymentary, skills[2], typeof(SkillBase));

            // Gán các hành động tấn công liên quan đến AttackBase
            AssignSkillToButton(nomalAttack, skills[3], typeof(AttackBase));
            AssignSkillToButton(killSoldier, skills[4], typeof(AttackBase));
            AssignSkillToButton(pushPillar, skills[5], typeof(AttackBase));

            // Gán các kỹ năng SkillBase còn lại
            AssignSkillToButton(skillButton1, skills[6], typeof(SkillBase));
            AssignSkillToButton(skillButton2, skills[7], typeof(SkillBase));
            AssignSkillToButton(skillButton3, skills[8], typeof(SkillBase));
        }
        else
        {
            Debug.LogWarning("Không tìm thấy kỹ năng cho hero: " + hero.name);
        }
    }

    // Gán kỹ năng hoặc hành động tấn công vào button
    private void AssignSkillToButton(Button button, Type skillType, Type baseType)
    {
        if (button != null)
        {
            // Kiểm tra xem button có component kế thừa từ baseType (SkillBase hoặc AttackBase) không
            Component skill = button.GetComponent(baseType);
            if (skill == null)
            {
                skill = button.gameObject.AddComponent(skillType);  // Thêm script kỹ năng hoặc hành động tấn công vào button
                Debug.Log($"{baseType.Name} {skillType.Name} has been added to {button.name}");
            }

            if (skill is SkillBase && baseType == typeof(SkillBase))
            {
                (skill as SkillBase)?.SetHero(hero); // Gán hero cho SkillBase
            }
            else if (skill is AttackBase && baseType == typeof(AttackBase))
            {
                (skill as AttackBase)?.SetHero(hero); // Gán hero cho AttackBase
            }
            else
            {
                Debug.LogError($"Không thể gán hero cho {skillType.Name} vì nó không kế thừa {baseType.Name}");
            }
        }
        else
        {
            Debug.LogError($"Button is null when trying to assign {baseType.Name} {skillType.Name}");
        }
    }
}
