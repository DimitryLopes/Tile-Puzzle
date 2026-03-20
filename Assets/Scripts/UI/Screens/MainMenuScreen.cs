using System;
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

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        optionsScreenButton.onClick.AddListener(OnOptionsScreenButtonClicked);
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
