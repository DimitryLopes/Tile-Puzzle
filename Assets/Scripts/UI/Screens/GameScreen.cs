using UnityEngine;

public class GameScreen : UIScreen<GameScreenController>
{
    [SerializeField]
    private Puzzle puzzle;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
    }

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
        EventManager.OnFloatingPiecesAnimationFinished.AddListener(OnFloatingPiecesAnimationFinished);
        puzzle.StartGame(Controller.PuzzleName, Controller.Canvas);
    }

    private void OnFloatingPiecesAnimationFinished()
    {
        puzzle.ShowPieces();
        puzzle.UpdateInteractables();
    }
}
