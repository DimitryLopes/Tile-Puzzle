
using System;

public class GameOverScreenController : ScreenController
{
    public readonly bool IsWin;
    public readonly Action OnExitButtonClicked;
    public readonly Action OnPlayButtonClicked;

    public GameOverScreenController(bool isWin, Action onPlayButtonClicked, Action onExitButtonClicked)
    {
        IsWin = isWin;
        OnPlayButtonClicked = onPlayButtonClicked;
        OnExitButtonClicked = onExitButtonClicked;
    }
}
