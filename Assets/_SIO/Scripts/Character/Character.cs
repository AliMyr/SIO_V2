using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;

    public CharacterData CharacterData => characterData;
    public CharacterController CharacterController => characterController;
    public Transform CharacterTransform => characterTransform;

    public IHealthComponent HealthComponent { get; private set; }
    public IMovableComponent MovableComponent { get; private set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        HealthComponent = new HealthComponent();
        HealthComponent.Initialize(this);

        MovableComponent = new DefaultMovableComponent();
        MovableComponent.Initialize(this);
    }

    protected abstract void Update();
}
