using System;
using UnityEngine;

public class HealthComponent : IHealthComponent
{
    private Character selfCharacter;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        MaxHealth = selfCharacter.CharacterData.MaxHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage) => ChangeHealth(-damage);
    public void Heal(float amount) => ChangeHealth(amount);

    private void ChangeHealth(float amount)
    {
        if (amount == 0) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        OnCharacterHealthChange?.Invoke(selfCharacter);

        if (CurrentHealth <= 0) Die();

    }

    private void Die()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log($"{selfCharacter.name} is dead");
    }
}
