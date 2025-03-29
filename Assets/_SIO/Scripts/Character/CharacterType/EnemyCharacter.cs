using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState aiState;
    public override Character CharacterTarget => 
        GameManager.Instance.CharacterFactory.Player;

    public IAttackComponent AttackComponent { get; protected set; }

    public override void Initialize()
    {
        base.Initialize();

        AttackComponent = new DefaultEnemyAttackComponent();
        AttackComponent.Initialize(this);
    }

    protected override void Update()
    {
        if (HealthComponent.CurrentHealth <= 0 || CharacterTarget == null)
            return;

        switch (aiState)
        {
            case AiState.Idle:
                return;

            case AiState.MoveToTarget:
                Vector3 moveDirection = (CharacterTarget.CharacterTransform.position -
                                         CharacterTransform.position).normalized;

                MovableComponent.Move(moveDirection);
                MovableComponent.Rotate(moveDirection);

                if (Vector3.Distance(CharacterTransform.position, CharacterTarget.CharacterTransform.position) <= AttackComponent.AttackRange)
                {
                    AttackComponent.Attack(CharacterTarget);
                }
                return;
        }
    }
}
