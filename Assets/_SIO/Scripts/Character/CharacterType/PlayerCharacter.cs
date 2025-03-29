using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override Character CharacterTarget
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            foreach (Character character in GameManager.Instance.CharacterFactory.ActiveCharacters)
            {
                if (character.CharacterType == CharacterType.Player || character.HealthComponent.CurrentHealth <= 0)
                    continue;

                float distance = Vector3.Distance(character.CharacterTransform.position, CharacterTransform.position);
                if (distance < minDistance)
                {
                    target = character;
                    minDistance = distance;
                }
            }
            return target;
        }
    }

    public IAttackComponent AttackComponent { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        AttackComponent = new PlayerAttackComponent();
        AttackComponent.Initialize(this);
    }

    protected override void Update()
    {
        if (HealthComponent.CurrentHealth <= 0)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (CharacterTarget != null)
        {
            Vector3 rotationDirection = CharacterTarget.CharacterTransform.position - CharacterTransform.position;
            MovableComponent.Rotate(rotationDirection);

            if (Vector3.Distance(CharacterTransform.position, CharacterTarget.CharacterTransform.position) <= AttackComponent.AttackRange)
            {
                AttackComponent.Attack(CharacterTarget);
            }
        }
        else
        {
            MovableComponent.Rotate(moveDirection);
        }

        MovableComponent.Move(moveDirection);
    }
}
