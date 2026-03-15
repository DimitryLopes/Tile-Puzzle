using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    private void Start()
    {
        var controller = new MainMenuScreenController(StartGame, ExitGame);
        ScreenManager.Instance.Show<MainMenuScreen>(controller);
    }

    public void StartGame()
    {
        var controller = new GameScreenController("Gift", canvas);
        ScreenManager.Instance.Show<GameScreen>(controller);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
