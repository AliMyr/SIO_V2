using System.Collections;
using System.Collections.Generic;
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
        if (attackTarget == null || selfCharacter == null || Time.time - lastAttackTime < AttackCooldown)
            return;

        if (Vector3.Distance(selfCharacter.CharacterTransform.position, 
            attackTarget.CharacterTransform.position) > AttackRange)
            return;

        selfCharacter.CharacterTransform.LookAt(attackTarget.CharacterTransform);

        MeleeAttack(attackTarget);

        lastAttackTime = Time.time;
    }

    private void MeleeAttack(Character attackTarget)
    {
        attackTarget.HealthComponent.TakeDamage(AttackDamage);
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
    }
}
