using System;
using UnityEngine;

public class ScoreSystem
{
    private const string SaveKey = "MaxScore";

    public event Action<int> OnScoreUpdated;

    public int Score { get; private set; }
    public int MaxScore { get; private set; }
    public bool IsNewScoreRecord { get; private set; }

    public void Initialize()
    {
        MaxScore = PlayerPrefs.GetInt(SaveKey, 0);
        Reset();
    }

    public void Reset()
    {
        Score = 0;
        IsNewScoreRecord = false;
    }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        Score += amount;
        OnScoreUpdated?.Invoke(Score);
    }

    public void SaveMaxScore()
    {
        if (Score <= MaxScore) return;

        MaxScore = Score;
        PlayerPrefs.SetInt(SaveKey, MaxScore);
        IsNewScoreRecord = true;
    }
}
