using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters = new();
    private List<Character> activeCharacters = new();

    public Character Player { get; private set; }
    public List<Character> ActiveCharacters => activeCharacters;

    public Character GetCharacter(CharacterType type)
    {
        if (!disabledCharacters.TryGetValue(type, out var queue))
        {
            queue = new Queue<Character>();
            disabledCharacters[type] = queue;
        }

        Character character = queue.Count > 0 ? queue.Dequeue() : InstantiateCharacter(type);

        if (character == null)
            return null;

        activeCharacters.Add(character);
        return character;
    }

    public void ReturnCharacter(Character character)
    {
        if (character == null)
            return;

        if (!disabledCharacters.ContainsKey(character.CharacterType))
        {
            disabledCharacters[character.CharacterType] = new Queue<Character>();
        }

        disabledCharacters[character.CharacterType].Enqueue(character);
        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = type switch
        {
            CharacterType.Player => Instantiate(playerCharacterPrefab),
            CharacterType.DefaultEnemy => Instantiate(enemyCharacterPrefab),
            _ => null
        };

        if (character == null)
        {
            Debug.LogError($"Unknown character type: {type}");
            return null;
        }

        if (type == CharacterType.Player)
            Player = character;

        return character;
    }
}
