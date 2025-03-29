using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;

    private ScoreSystem scoreSystem;
    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;
    public ScoreSystem ScoreSystem => scoreSystem;
    public float GameSessionTime => gameSessionTime;
    public bool IsGameActive => isGameActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else 
        {
            Destroy(this.gameObject);
            return;
        }   
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive)
            return;

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;

        scoreSystem.StartGame();
        isGameActive=true;
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        if (timeBetweenEnemySpawn <= 0)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeMinutes)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        if (deathCharacter == null) return;

        deathCharacter.HealthComponent.OnCharacterDeath -= CharacterDeathHandler;
        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deathCharacter);

        switch (deathCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;

            case CharacterType.DefaultEnemy:
                scoreSystem.AddScore(deathCharacter.CharacterData.ScoreCost);
                break;
        }
    }

    private void SpawnEnemy()
    {
        if (characterFactory.Player == null)
            return;

        Character enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x +
            GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

        float GetOffset()
        {
            float offset = Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset);
            return Random.value > 0.5f ? offset : -offset;
        }
    }

    private void GameVictory()
    {
        EndGame();
        Debug.Log("Victory");
    }

    private void GameOver()
    {
        EndGame();
        Debug.Log("Defeat");
    }

    private void EndGame()
    {
        isGameActive = false ;
        scoreSystem.EndGame();
    }
}
