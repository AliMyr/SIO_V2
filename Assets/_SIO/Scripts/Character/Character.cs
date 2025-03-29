using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;

    public CharacterType CharacterType => characterType;
    public CharacterData CharacterData => characterData;
    public CharacterController CharacterController => characterController;
    public Transform CharacterTransform => characterTransform;
    public virtual Character CharacterTarget => null;

    public IHealthComponent HealthComponent { get; private set; }
    public IMovableComponent MovableComponent { get; private set; }

    public virtual void Initialize()
    {
        HealthComponent = new HealthComponent();
        HealthComponent.Initialize(this);

        MovableComponent = new DefaultMovableComponent();
        MovableComponent.Initialize(this);
    }

    protected abstract void Update();
}
