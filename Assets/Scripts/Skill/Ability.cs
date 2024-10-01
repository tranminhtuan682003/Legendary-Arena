using UnityEngine;

public abstract class Ability
{
    public string abilityName;  // Tên kỹ năng
    public float cooldown;  // Thời gian hồi chiêu
    public float manaCost;  // Chi phí mana để kích hoạt kỹ năng
    private float lastUsedTime = -1f;  // Đặt giá trị khởi tạo để không rơi vào cooldown ngay

    // Thuộc tính công khai để truy cập thời gian sử dụng cuối cùng
    public float LastUsedTime => lastUsedTime;

    // Kiểm tra xem kỹ năng có đang trong thời gian hồi chiêu hay không
    public bool IsOnCooldown
    {
        get
        {
            return lastUsedTime >= 0 && Time.time - lastUsedTime < cooldown;  // Thêm kiểm tra để tránh lỗi
        }
    }

    public virtual void Activate()
    {
        if (!IsOnCooldown)
        {
            lastUsedTime = Time.time;  // Cập nhật thời gian sử dụng cuối cùng
            UseAbility();  // Kích hoạt kỹ năng
        }
        else
        {
            float cooldownRemaining = cooldown - (Time.time - lastUsedTime);
            Debug.Log(abilityName + " is on cooldown for " + cooldownRemaining.ToString("F1") + " seconds.");
        }
    }

    protected abstract void UseAbility();  // Phương thức này sẽ được các lớp con triển khai
}
