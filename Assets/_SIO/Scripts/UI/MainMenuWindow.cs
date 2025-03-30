using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;

    public override void Initialize()
    {
        startGameButton.onClick.AddListener(StartGame);
        optionsGameButton.onClick.AddListener(OpenOptions);
    }

    protected override void OpenEnd() => SetButtonsInteractable(true);
    protected override void CloseStart() => SetButtonsInteractable(false);

    private void StartGame()
    {
        var gameManager = GameManager.Instance;
        gameManager.StartGame();
        gameManager.WindowsService.ShowWindow<GameplayWindow>(true);
        Hide(false);
    }

    private void OpenOptions()
    {
        Hide(false);
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(true);
    }

    private void SetButtonsInteractable(bool state)
    {
        startGameButton.interactable = state;
        optionsGameButton.interactable = state;
    }
}
