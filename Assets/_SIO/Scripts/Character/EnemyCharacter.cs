using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private Character characterTarget;
    [SerializeField] private AiState aiState;

    public IAttackComponent AttackComponent { get; protected set; }

    public override void Initialize()
    {
        base.Initialize();

        AttackComponent = new DefaultEnemyAttackComponent();
        AttackComponent.Initialize(this);
    }

    protected override void Update()
    {
        if(HealthComponent.CurrentHealth <= 0 || characterTarget == null)
            return;

        switch (aiState)
        {
            case AiState.Idle:
                return;

            case AiState.MoveToTarget:
                Vector3 moveDirection = 
                    (characterTarget.CharacterTransform.position - 
                    CharacterTransform.position).normalized;

                MovableComponent.Move(moveDirection);
                MovableComponent.Rotate(moveDirection);

                AttackComponent.Attack(characterTarget);

                return;
        }
    }
}
