using System;

public interface IHealthComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void TakeDamage(float damage);
    void Heal(float amount);
}
