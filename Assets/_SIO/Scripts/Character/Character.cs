using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    public CharacterData CharacterData => characterData;
    public IHealthComponent HealthComponent { get; private set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        HealthComponent = new HealthComponent();
        HealthComponent.Initialize(this);
    }
}
