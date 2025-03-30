using UnityEngine;
using UnityEngine.UI;

public class DefeatWindow : Window
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMainMenuButton;

    public override void Initialize()
    {
        base.Initialize();
        restartButton.onClick.AddListener(RestartGame);
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void RestartGame()
    {
        Hide(true);
        var windowsService = GameManager.Instance.WindowsService;
        windowsService.ShowWindow<GameplayWindow>(false);
        GameManager.Instance.StartGame();
    }

    private void ReturnToMainMenu()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }
}
