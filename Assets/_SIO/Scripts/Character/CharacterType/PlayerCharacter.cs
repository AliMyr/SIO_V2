using UnityEngine;

public class PlayerCharacter : Character
{
    public override Character CharacterTarget => GetClosestEnemy();

    public override void Initialize(
        IHealthComponent healthComponent,
        IMovableComponent movableComponent,
        IAttackComponent attackComponent)
    {
        base.Initialize(healthComponent, movableComponent, attackComponent);
    }

    protected override void Update()
    {
        if (HealthComponent.CurrentHealth <= 0)
            return;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        MovableComponent.Move(moveDirection);

        if (CharacterTarget != null)
        {
            Vector3 targetDirection = CharacterTarget.CharacterTransform.position - CharacterTransform.position;
            MovableComponent.Rotate(targetDirection);

            if (Vector3.Distance(CharacterTransform.position, CharacterTarget.CharacterTransform.position) <= AttackComponent.AttackRange)
            {
                AttackComponent.Attack(CharacterTarget);
            }
        }
        else
        {
            MovableComponent.Rotate(moveDirection);
        }
    }

    private Character GetClosestEnemy()
    {
        Character closest = null;
        float minDistance = float.MaxValue;

        foreach (Character character in GameManager.Instance.CharacterFactory.ActiveCharacters)
        {
            if (character.CharacterType == CharacterType.Player || character.HealthComponent.CurrentHealth <= 0)
                continue;

            float distance = Vector3.Distance(character.CharacterTransform.position, CharacterTransform.position);
            if (distance < minDistance)
            {
                closest = character;
                minDistance = distance;
            }
        }

        return closest;
    }
}
