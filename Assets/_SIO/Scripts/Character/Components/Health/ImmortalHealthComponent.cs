using System;

public class ImmortalHealthComponent : IHealthComponent
{
    public float MaxHealth => float.MaxValue;
    public float CurrentHealth => float.MaxValue;

    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;

    public void TakeDamage(float damage) { }
    public void Heal(float amount) { }
    public void Initialize(Character selfCharacter) { }
}
