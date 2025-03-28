public class ImmortalHealthComponent : IHealthComponent
{
    public float MaxHealth => float.MaxValue;
    public float CurrentHealth => float.MaxValue;

    public void TakeDamage(float damage) { }
    public void Heal(float amount) { }
    public void Initialize(Character selfCharacter) { }
}
