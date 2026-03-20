using TMPro;
using UnityEngine;

public class GameScreen : UIScreen<GameScreenController>
{
    [SerializeField]
    private TextMeshProUGUI eventText;
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
        puzzle.SetupGame(Controller.PuzzleName, Controller.Canvas);
    }

    private void OnFloatingPiecesAnimationFinished()
    {
        puzzle.ShowPieces();
        puzzle.UpdateInteractables();
        puzzle.StartGame();
    }
}
