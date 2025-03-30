using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text coinsText;

    private Character player;
    private ScoreSystem scoreSystem;

    public override void Initialize()
    {
        player = GameManager.Instance.CharacterFactory.Player;
        scoreSystem = GameManager.Instance.ScoreSystem;
    }

    protected override void OpenStart()
    {
        base.OpenStart();

        if (player == null) return;

        UpdateHealthVisual(player);
        player.HealthComponent.OnCharacterHealthChange += UpdateHealthVisual;

        UpdateScore(scoreSystem.Score);
        scoreSystem.OnScoreUpdated += UpdateScore;
    }

    protected override void CloseStart()
    {
        base.CloseStart();
        if (player == null) return;

        player.HealthComponent.OnCharacterHealthChange -= UpdateHealthVisual;
        scoreSystem.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateHealthVisual(Character character)
    {
        int health = (int)character.HealthComponent.CurrentHealth;
        int healthMax = (int)character.HealthComponent.MaxHealth;

        healthText.text = $"{health}/{healthMax}";
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
    }

    private void UpdateScore(int score) => coinsText.text = $"Score: {score}";

    private void Update()
    {
        float sessionTime = GameManager.Instance.GameSessionTime;
        int minutes = (int)(sessionTime / 60);
        int seconds = (int)(sessionTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
