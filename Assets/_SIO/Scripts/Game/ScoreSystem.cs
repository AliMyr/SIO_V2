using System;
using UnityEngine;

public class ScoreSystem
{
    private const string SaveKey = "MaxScore";

    public event Action<int> OnScoreUpdated;

    public int Score { get; private set; }
    public int MaxScore { get; private set; }
    public bool IsNewScoreRecord { get; private set; }

    public void StartGame()
    {
        Score = 0;
        MaxScore = PlayerPrefs.GetInt(SaveKey, 0);
        IsNewScoreRecord = false;
    }

    public void EndGame()
    {
        if (Score <= MaxScore) return;

        MaxScore = Score;
        PlayerPrefs.SetInt(SaveKey, MaxScore);
        IsNewScoreRecord = true;
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        Score += amount;
        OnScoreUpdated?.Invoke(Score);
    }
}
