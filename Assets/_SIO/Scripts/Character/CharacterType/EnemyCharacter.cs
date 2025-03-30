using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState aiState;

    public override Character CharacterTarget => GameManager.Instance.CharacterFactory.Player;

    protected override void Update()
    {
        if (HealthComponent.CurrentHealth <= 0 || CharacterTarget == null)
            return;

        if (aiState == AiState.MoveToTarget)
        {
            Vector3 moveDirection = (CharacterTarget.CharacterTransform.position - CharacterTransform.position).normalized;
            MovableComponent.Move(moveDirection);
            MovableComponent.Rotate(moveDirection);

            if (Vector3.Distance(CharacterTransform.position, CharacterTarget.CharacterTransform.position) <= AttackComponent.AttackRange)
            {
                AttackComponent.Attack(CharacterTarget);
            }
        }
    }
}
