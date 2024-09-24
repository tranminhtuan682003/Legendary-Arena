public interface IHealth
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    void TakeDamage(float amount);
    void Heal(float amount);
    bool IsDead { get; }
    void Dead();
}
