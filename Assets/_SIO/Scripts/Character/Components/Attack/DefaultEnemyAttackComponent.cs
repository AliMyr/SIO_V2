using UnityEngine;

public class DefaultEnemyAttackComponent : IAttackComponent
{
    private Character selfCharacter;
    private float lastAttackTime;

    public float AttackCooldown => selfCharacter.CharacterData.AttackCooldown;
    public float AttackRange => selfCharacter.CharacterData.AttackRange;
    public float AttackDamage => selfCharacter.CharacterData.AttackDamage;

    public void Attack(Character attackTarget)
    {
        if (attackTarget == null || attackTarget.HealthComponent.CurrentHealth <= 0)
            return;

        if (Time.time - lastAttackTime < AttackCooldown)
            return;

        if (Vector3.Distance(selfCharacter.CharacterTransform.position, attackTarget.CharacterTransform.position) > AttackRange)
            return;

        selfCharacter.CharacterTransform.LookAt(attackTarget.CharacterTransform);

        attackTarget.HealthComponent.TakeDamage(AttackDamage);
        lastAttackTime = Time.time;
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
    }
}
