
using System;
using UnityEngine;

public class GameScreenController : ScreenController
{
    public readonly string PuzzleName;
    public readonly Canvas Canvas;
    public readonly Action OnReturnButtonClicked;

    public GameScreenController(string puzzleName, Canvas canvas, Action onReturnButtonClicked)
    {
        PuzzleName = puzzleName;
        Canvas = canvas;
        OnReturnButtonClicked = onReturnButtonClicked;
    }
}
