using System;

public class MainMenuScreenController : ScreenController
{
    public readonly Action StartGame;
    public readonly Action ExitGame;
    public readonly Action OptionsScreen;

    public MainMenuScreenController(Action startGame, Action exitGame, Action optionsScreen)
    {
        StartGame = startGame;
        ExitGame = exitGame;
        OptionsScreen = optionsScreen;
    }
}
