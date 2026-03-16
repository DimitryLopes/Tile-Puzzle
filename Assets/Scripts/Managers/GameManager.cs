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
    private List<GameEvent> gameEvents;

    private bool isPlaying = false;
    private bool isEventActive = false;
    private float eventTimer = 10f;
    private GameEvent currentEvent = null;

    private void Start()
    {
        EventManager.OnGameOver.AddListener(OnGameOver);
        EventManager.OnEventEnded.AddListener(OnEventEnded);

        var controller = new MainMenuScreenController(StartGame, ExitGame);
        ScreenManager.Instance.Show<MainMenuScreen>(controller);
    }

    private void Update()
    {
        if(isPlaying && !isEventActive)
        {
            eventTimer -= Time.deltaTime;
            if(eventTimer <= 0f)
            {
                isEventActive = true;
                eventTimer = Random.Range(minEventTimer, maxEventTimer);
                GameEvent evt = gameEvents.GetRandom();
                evt.StartEvent();
                currentEvent = evt;
            }
        }

        if (isEventActive)
        {
            currentEvent.UpdateEvent();
        }
    }

    private void OnEventEnded(bool isWin)
    {
        isEventActive = false;
        if (isWin) return;
        EventManager.OnGameOver.Invoke(isWin);
    }

    public void StartGame()
    {
        isPlaying = true;
        var controller = new GameScreenController("Gift", canvas);
        ScreenManager.Instance.Show<GameScreen>(controller);
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
