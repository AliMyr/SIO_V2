using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private WindowsService windowsService;

    private ScoreSystem scoreSystem;
    private float gameSessionTime;
    private float enemySpawnTimer;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;
    public WindowsService WindowsService => windowsService;
    public ScoreSystem ScoreSystem => scoreSystem;
    public float GameSessionTime => gameSessionTime;
    public bool IsGameActive => isGameActive;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        isGameActive = false;
        windowsService.Initialize();
    }
    

    public void StartGame()
    {
        if (isGameActive) return;

        var player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        player.Initialize(
            new HealthComponent(),
            new DefaultMovableComponent(),
            new PlayerAttackComponent()
        );

        player.HealthComponent.OnCharacterDeath += OnCharacterDeath;

        gameSessionTime = 0;
        enemySpawnTimer = gameData.TimeBetweenEnemySpawn;

        scoreSystem.StartGame();
        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive) return;

        gameSessionTime += Time.deltaTime;
        enemySpawnTimer -= Time.deltaTime;

        if (enemySpawnTimer <= 0)
        {
            SpawnEnemy();
            enemySpawnTimer = gameData.TimeBetweenEnemySpawn;
        }

        if (gameSessionTime >= gameData.SessionTimeSeconds)
        {
            EndGame(true);
        }
    }

    private void OnCharacterDeath(Character character)
    {
        if (character == null) return;

        character.HealthComponent.OnCharacterDeath -= OnCharacterDeath;
        character.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(character);

        if (character.CharacterType == CharacterType.Player)
        {
            EndGame(false);
        }
        else if (character.CharacterType == CharacterType.DefaultEnemy)
        {
            scoreSystem.AddScore(character.CharacterData.ScoreCost);
        }
    }

    private void SpawnEnemy()
    {
        if (characterFactory.Player == null) return;

        var enemy = characterFactory.GetCharacter(CharacterType.DefaultEnemy);
        var playerPosition = characterFactory.Player.transform.position;

        enemy.transform.position = GetSpawnPosition(playerPosition);
        enemy.gameObject.SetActive(true);

        enemy.Initialize(
            new HealthComponent(),
            new DefaultMovableComponent(),
            new DefaultEnemyAttackComponent()
        );

        enemy.HealthComponent.OnCharacterDeath += OnCharacterDeath;
    }

    private Vector3 GetSpawnPosition(Vector3 playerPosition)
    {
        float offsetX = GetOffset();
        float offsetZ = GetOffset();
        return new Vector3(playerPosition.x + offsetX, 0, playerPosition.z + offsetZ);
    }

    private float GetOffset() => Random.Range(gameData.MinSpawnOffset, gameData.MaxSpawnOffset) * (Random.value > 0.5f ? 1 : -1);

    private void EndGame(bool isVictory)
    {
        isGameActive = false;
        scoreSystem.EndGame();

        WindowsService.HideWindow<GameplayWindow>(true);
        WindowsService.ShowWindow(isVictory ? typeof(VictoryWindow) : typeof(DefeatWindow), false);
    }
}
