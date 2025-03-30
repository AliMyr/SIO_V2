using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow : Window
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private Button closeButton;

    public override void Initialize()
    {
        closeButton.onClick.AddListener(CloseOptions);
    }

    private void CloseOptions()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }
}
