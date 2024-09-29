public interface IHealthSoldier
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    void TakeDamage(float damage);
    void Heal(float amount);
    bool IsAlive();
    void Die();
}
