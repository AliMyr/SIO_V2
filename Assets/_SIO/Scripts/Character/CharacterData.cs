using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private int scoreCost;
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;

    public int ScoreCost => scoreCost;
    public float MaxHealth => maxHealth;
    public float Speed => speed;
    public float TurnSmoothTime => turnSmoothTime;
    public float AttackCooldown => attackCooldown;
    public float AttackRange => attackRange;
    public float AttackDamage => attackDamage;
}
