using System;
using UnityEngine;

public class ScoreSystem
{
    private const string SAVE_NAME = "MaxScore";

    public event Action<int> OnScoreUpdated;

    public int Score {  get; private set; }
    public int MaxScore { get; private set; }
    public bool IsNewScoreRecord { get; private set; }

    public void CharacterDeathHandler(Character character)
    {
        Score++;
        OnScoreUpdated?.Invoke(Score);
    }

    public void StartGame()
    {
        Score = 0;
        MaxScore = PlayerPrefs.GetInt(SAVE_NAME, 0);
        IsNewScoreRecord = false;
    }

    public void EndGame()
    {
        if (Score > MaxScore)
        {
            MaxScore = Score;
            PlayerPrefs.SetInt(SAVE_NAME, MaxScore);
            IsNewScoreRecord = true;
        }
    }

    public void AddScore (int earnedScore)
    {
        Score += earnedScore;
    }
}
