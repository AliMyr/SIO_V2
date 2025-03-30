using System;
using UnityEngine;

public class HealthComponent : IHealthComponent
{
    private Character selfCharacter;
    private float currentHealth;

    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;

    public float MaxHealth { get; private set; }
    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnCharacterHealthChange?.Invoke(selfCharacter);
            if (currentHealth <= 0) Die();
        }
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        MaxHealth = selfCharacter.CharacterData.MaxHealth;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
            CurrentHealth -= damage;
    }

    public void Heal(float amount)
    {
        if (amount > 0)
            CurrentHealth += amount;
    }

    private void Die()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log($"{selfCharacter.name} is dead");
    }
}
