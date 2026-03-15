
using UnityEngine;

public class GameScreen : UIScreen<GameScreenController>
{
    [SerializeField]
    private Puzzle puzzle;

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
        puzzle.StartGame(Controller.PuzzleName, Controller.Canvas);
    }
}
