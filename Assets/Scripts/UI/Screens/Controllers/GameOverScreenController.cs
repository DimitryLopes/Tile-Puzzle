
using System;

public class GameOverScreenController : ScreenController
{
    public readonly string SubtitleText;
    public readonly bool IsWin;
    public readonly Action OnExitButtonClicked;
    public readonly Action OnPlayButtonClicked;

    public GameOverScreenController(bool isWin, Action onPlayButtonClicked,
        Action onExitButtonClicked, string subtitleText)
    {
        IsWin = isWin;
        OnPlayButtonClicked = onPlayButtonClicked;
        OnExitButtonClicked = onExitButtonClicked;
        SubtitleText = subtitleText;
    }
}
