using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private float minEventTimer;
    [SerializeField]
    private float maxEventTimer;
    [SerializeField]
    private List<EventData> eventDatas;

    private bool isPlaying = false;
    private bool isEventActive = false;
    private float eventTimer = 10f;
    private GameEvent currentEvent = null;
    private Board currentBoard;

    public static GameManager Instance { get; private set; }

    private GameEvent ClassicMode => eventDatas[0].gameEvent;
    public Puzzle Puzzle { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        EventManager.OnGameOver.AddListener(OnGameOver);
        EventManager.OnGameEventEnded.AddListener(OnGameEventEnded);
        EventManager.OnGameStarted.AddListener(OnGameStarted);
        SelectBoard(Board.Gift);
        ShowMainMenu();
    }

    private void Update()
    {
        if(isPlaying && !isEventActive)
        {
            eventTimer -= Time.deltaTime;
            if(eventTimer <= 0f)
            {
                SelectNewEvent();
            }
        }

        if (isEventActive)
        {
            currentEvent.UpdateEvent();
        }        
    }

    private void SelectNewEvent()
    {
        eventTimer = UnityEngine.Random.Range(minEventTimer, maxEventTimer);
        var availableEvents = new List<GameEvent>();
        foreach (var e in eventDatas)
        {
            if (e.gameEvent.IsEventActive)
            {
                availableEvents.Add(e.gameEvent);
            }
        }
        GameEvent evt = availableEvents.GetRandom();
        StartEvent(evt);
    }

    private void StartEvent(GameEvent evt)
    {
        isEventActive = true;
        evt.StartEvent();
        currentEvent = evt;
        EventManager.OnGameEventStarted.Invoke(evt);
    }

    private void OnGameEventEnded(GameEvent evt, bool isWin)
    {
        isEventActive = false;
        if (isWin) return;
        EventManager.OnGameOver.Invoke(isWin);
    }

    private void ShowMainMenu()
    {
        var controller = new MainMenuScreenController(ShowGameSelectionScreen, ExitGame);
        ScreenManager.Instance.Show<MainMenuScreen>(controller);
    }

    public void ShowGameSelectionScreen()
    {
        var controller = new GameModeScreenController(eventDatas,
            OnEventToggled, StartGame, ShowMainMenu, SelectBoard,
            currentBoard);
        ScreenManager.Instance.Show<GameModeScreen>(controller);
    }

    private void SelectBoard(Board board)
    {
        currentBoard = board;
        EventManager.OnBoardSelected.Invoke(board);
    }

    private void OnEventToggled(EventID id)
    {
        for(int i = 0; i < eventDatas.Count; i++)
        {
            var data = eventDatas[i];
            if (data.eventID != id) continue;

            data.gameEvent.ToggleEvent(!data.gameEvent.IsEventActive);
            eventDatas[i] = data;
            return;
        }

        Debug.LogError("Event with ID:" + id + " was not found");
    }

    public void StartGame()
    {
        isPlaying = true;
        var controller = new GameScreenController(currentBoard.ToString(), canvas);
        ScreenManager.Instance.Show<GameScreen>(controller);
    }

    private void OnGameStarted(Puzzle puzzle)
    {
        Puzzle = puzzle;
        if (ClassicMode.IsEventActive)
        {
            StartEvent(ClassicMode);
        }
    }
    private void OnGameOver(bool isVictory)
    {
        isPlaying = false;
        var controller = new GameOverScreenController(isVictory, StartGame, ExitGame);
        ScreenManager.Instance.Show<GameOverScreen>(controller);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

[Serializable]
public struct EventData
{
    [SerializeField]
    public EventID eventID;
    [SerializeField]
    public Sprite eventIcon;
    [SerializeField]
    public GameEvent gameEvent;
}
