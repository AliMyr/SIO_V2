using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Slider healthSlider;

    [Space]
    [SerializeField]
    private Slider experienceSlider;

    [Space]
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private TMP_Text coinsText;

    public override void Initialize()
    {

    }

    protected override void OpenStart()
    {
        base.OpenStart();
        Character player = GameManager.Instance.CharacterFactory.Player;

        UpdateHealthVisual(player);
        player.HealthComponent.OnCharacterHealthChange += UpdateHealthVisual;

        UpdateScore(GameManager.Instance.ScoreSystem.Score);
        GameManager.Instance.ScoreSystem.OnScoreUpdated += UpdateScore;
    }

    protected override void CloseStart()
    {
        base.CloseStart();

        Character player = GameManager.Instance.CharacterFactory.Player;
        if (player == null)
            return;

        player.HealthComponent.OnCharacterHealthChange -= UpdateHealthVisual;
        GameManager.Instance.ScoreSystem.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateHealthVisual(Character character)
    {
        int health = (int)character.HealthComponent.CurrentHealth;
        int healthMax = (int)character.HealthComponent.MaxHealth;

        healthText.text = health + "/" + healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
    }

    private void UpdateScore(int scoreCount)
    {
        coinsText.text = "score: " + scoreCount;
    }

    private void Update()
    {
        float sessionTime = GameManager.Instance.GameSessionTime;
        int minutes = (int)(sessionTime / 60);
        int seconds = (int)(sessionTime % 60);
        string zero = "0";

        timerText.text = minutes + ":" + ((seconds < 10) ? zero : "") + seconds;
    }
}
