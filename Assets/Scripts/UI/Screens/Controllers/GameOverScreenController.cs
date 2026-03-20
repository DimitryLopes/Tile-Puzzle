
using System;

public class GameOverScreenController : ScreenController
{
    public readonly string SubtitleText;
    public readonly bool IsWin;
    public readonly Action OnExitButtonClicked;
    public readonly Action OnPlayButtonClicked;
    public readonly Action OnMainMenuButtonClicked;

    public GameOverScreenController(bool isWin, Action onPlayButtonClicked,
        Action onExitButtonClicked, Action onMainMenuButtonClicked, string subtitleText)
    {
        IsWin = isWin;
        OnPlayButtonClicked = onPlayButtonClicked;
        OnExitButtonClicked = onExitButtonClicked;
        OnMainMenuButtonClicked = onMainMenuButtonClicked;
        SubtitleText = subtitleText;
    }
}
