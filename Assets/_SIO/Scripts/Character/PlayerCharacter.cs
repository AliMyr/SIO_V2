using UnityEngine;

public class PlayerCharacter : Character
{
    public override void Initialize()
    {
        base.Initialize();
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
    }
}
