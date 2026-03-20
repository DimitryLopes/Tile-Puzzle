using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public static UnityEvent<IScreen> OnScreenAfterHideEvent = new UnityEvent<IScreen>();
    public static UnityEvent<IScreen> OnScreenAfterShowEvent = new UnityEvent<IScreen>();
    public static UnityEvent<IScreen> OnScreenBeforeHideEvent = new UnityEvent<IScreen>();
    public static UnityEvent<IScreen> OnScreenBeforeShowEvent = new UnityEvent<IScreen>();

    public static UnityEvent OnFloatingPiecesAnimationFinished = new UnityEvent();

    public static UnityEvent<Board> OnBoardSelected = new UnityEvent<Board>();

    public static UnityEvent<Puzzle> OnGameStarted = new UnityEvent<Puzzle>();
    public static UnityEvent<GameEvent, bool> OnGameOver = new UnityEvent<GameEvent, bool>();
    public static UnityEvent<PuzzlePiece[]> OnPuzzleShuffled = new UnityEvent<PuzzlePiece[]>();
    public static UnityEvent<GameEvent, bool> OnGameEventEnded = new UnityEvent<GameEvent, bool>();
    public static UnityEvent<GameEvent> OnGameEventStarted = new UnityEvent<GameEvent>();
    public static UnityEvent OnPieceMoved = new UnityEvent();

}