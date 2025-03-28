public interface IAttackComponent : ICharacterComponent
{
    float AttackCooldown { get; }
    float AttackRange { get; }
    float AttackDamage { get; }

    void Attack(Character attackTarget);
}