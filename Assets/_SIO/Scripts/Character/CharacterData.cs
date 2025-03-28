using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;

    public float MaxHealth => maxHealth;
    public float Speed => speed;
    public float TurnSmoothTime => turnSmoothTime;
}
