using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen<MainMenuScreenController>
{
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button exitGameButton;
    [SerializeField]
    private Button optionsScreenButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(PlayButtonSFX);
        exitGameButton.onClick.AddListener(PlayReturnButtonSFX);
        optionsScreenButton.onClick.AddListener(PlayButtonSFX);
    }

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        optionsScreenButton.onClick.AddListener(OnOptionsScreenButtonClicked);
    }

    protected override void OnAfterHide()
    {
        base.OnAfterHide();
        startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        optionsScreenButton.onClick.RemoveListener(OnOptionsScreenButtonClicked);
    }

    private void OnOptionsScreenButtonClicked()
    {
        Controller.OptionsScreen.Invoke();
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
