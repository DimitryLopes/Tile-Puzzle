
using UnityEngine;

public class GameScreenController : ScreenController
{
    public readonly string PuzzleName;
    public readonly Canvas Canvas;

    public GameScreenController(string puzzleName, Canvas canvas)
    {
        PuzzleName = puzzleName;
        Canvas = canvas;
    }
}
