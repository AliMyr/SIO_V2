using System;

public interface IHealthComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterDeath;
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void TakeDamage(float damage);
    void Heal(float amount);
}
