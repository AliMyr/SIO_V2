using UnityEngine;

public class DefaultMovableComponent : IMovableComponent
{
    private Character selfCharacter;
    private float turnSmoothVelocity;

    public float Speed { get; set; }
    public Vector3 Position => selfCharacter.CharacterTransform.position;

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        Speed = selfCharacter.CharacterData.Speed;
    }

    public void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude < Mathf.Epsilon) return;

        Vector3 movement = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0) * Vector3.forward;
        selfCharacter.CharacterController?.Move(movement * Speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        if (direction.sqrMagnitude < Mathf.Epsilon) return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(selfCharacter.CharacterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, selfCharacter.CharacterData.TurnSmoothTime);

        selfCharacter.CharacterTransform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
