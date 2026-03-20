using TMPro;
using UnityEngine;

public class GameScreen : UIScreen<GameScreenController>
{
    [SerializeField]
    private AnnouncementText eventText;
    [SerializeField]
    private Puzzle puzzle;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        EventManager.OnFloatingPiecesAnimationFinished.AddListener(OnFloatingPiecesAnimationFinished);
        EventManager.OnGameEventStarted.AddListener(OnGameEventStarted);
        EventManager.OnGameEventEnded.AddListener(OnGameEventEnded);
    }

    private void OnGameEventStarted(GameEvent evt)
    {
        eventText.ShowText(evt.StartMessage);
    }

    private void OnGameEventEnded(GameEvent evt, bool isWin)
    {
        if (!isWin) return;
        eventText.ShowText(evt.VictoryMessage);
    }

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
        puzzle.SetupGame(Controller.PuzzleName, Controller.Canvas);
    }

    protected override void OnAfterHide()
    {
        base.OnAfterHide();

        EventManager.OnFloatingPiecesAnimationFinished.RemoveListener(OnFloatingPiecesAnimationFinished);
        EventManager.OnGameEventStarted.RemoveListener(OnGameEventStarted);
        EventManager.OnGameEventEnded.RemoveListener(OnGameEventEnded);

    }
    private void OnFloatingPiecesAnimationFinished()
    {
        puzzle.ShowPieces();
        puzzle.UpdateInteractables();
        puzzle.StartGame();
    }
}
