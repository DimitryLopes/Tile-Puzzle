using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private void Start()
    {
        EventManager.OnGameOver.AddListener(OnGameOver);
        var controller = new MainMenuScreenController(StartGame, ExitGame);
        ScreenManager.Instance.Show<MainMenuScreen>(controller);
    }

    public void StartGame()
    {
        var controller = new GameScreenController("Gift", canvas);
        ScreenManager.Instance.Show<GameScreen>(controller);
    }

    private void OnGameOver(bool isVictory)
    {
        var controller = new GameOverScreenController(isVictory, StartGame, ExitGame);
        ScreenManager.Instance.Show<GameOverScreen>(controller);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
