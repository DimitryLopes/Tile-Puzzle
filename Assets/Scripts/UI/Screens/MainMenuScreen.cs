using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen<MainMenuScreenController>
{
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button exitGameButton;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
    }

    private void OnStartGameButtonClicked()
    {
        Controller.StartGame.Invoke();
    }

    private void OnExitGameButtonClicked()
    {
        Controller.ExitGame.Invoke();
    }
}
