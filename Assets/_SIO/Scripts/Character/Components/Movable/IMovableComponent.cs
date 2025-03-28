using UnityEngine;

public interface IMovableComponent : ICharacterComponent
{
    float Speed { get; set; }
    Vector3 Position { get; }

    void Move(Vector3 direction);
    void Rotate(Vector3 direction);
}
