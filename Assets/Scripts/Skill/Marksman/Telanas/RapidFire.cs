using UnityEngine;
public class RapidFire : Ability
{
    public RapidFire()
    {
        abilityName = "Rapid";
        cooldown = 5f;  // Thời gian hồi chiêu là 15 giây
        manaCost = 30f;
    }

    protected override void UseAbility()
    {
        // Logic kích hoạt kỹ năng Rapid Fire
        Debug.Log("Activating Rapid Fire!");
        // Có thể thêm logic để tăng tốc độ bắn hoặc tăng sát thương tạm thời
    }
}
