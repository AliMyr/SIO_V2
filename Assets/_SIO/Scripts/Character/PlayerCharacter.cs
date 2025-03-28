using UnityEngine;

public class PlayerCharacter : Character
{
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

        MovableComponent.Move(moveDirection);
        MovableComponent.Rotate(moveDirection);

        ProcessAutoAttack();
    }

    private void ProcessAutoAttack()
    {
        // Заглушка для логики авто-атаки
    }
}
