using System;
using UnityEngine;

public class HealthComponent : IHealthComponent
{
    private Character selfCharacter;
    private float maxHealth;
    private float currentHealth;

    public event Action<Character> OnCharacterDeath;

    public float MaxHealth => maxHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            if (currentHealth <= 0) SetDeath();
        }
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        maxHealth = selfCharacter.CharacterData.MaxHealth;
        currentHealth = maxHealth;
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

    private void SetDeath()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log("Character is dead");
    }
}
