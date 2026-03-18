
using System;
using UnityEngine;

public class GameScreen : UIScreen<GameScreenController>
{
    [SerializeField]
    private Puzzle puzzle;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        puzzle.gameObject.SetActive(false);
    }

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
        EventManager.OnFloatingPiecesAnimationFinished.AddListener(OnFloatingPiecesAnimationFinished);
    }

    private void OnFloatingPiecesAnimationFinished()
    {
        puzzle.gameObject.SetActive(true);
        puzzle.StartGame(Controller.PuzzleName, Controller.Canvas);
    }
}
