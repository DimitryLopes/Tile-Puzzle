using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : UIScreen<GameOverScreenController>
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI subtitleText;
    [SerializeField]
    private Button playAgainButton;
    [SerializeField]
    private Button exitButton;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    protected override void OnAfterHide()
    {
        base.OnAfterHide();
        playAgainButton.onClick.RemoveListener(OnPlayAgainButtonClicked);
        exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnExitButtonClicked()
    {
        Controller.OnExitButtonClicked.Invoke();
    }

    private void OnPlayAgainButtonClicked()
    {
        Controller.OnPlayButtonClicked.Invoke();
    }
}

