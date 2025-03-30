using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : Window
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private TMP_Text newRecordText;

    public override void Initialize()
    {
        base.Initialize();
        continueButton.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        var scoreSystem = GameManager.Instance.ScoreSystem;
        recordText.text = $"Score: {scoreSystem.Score}";
        newRecordText.gameObject.SetActive(scoreSystem.IsNewScoreRecord);
    }
}
