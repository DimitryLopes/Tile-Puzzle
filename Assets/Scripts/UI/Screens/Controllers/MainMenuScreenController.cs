using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenController : ScreenController
{
    public readonly Action StartGame;
    public readonly Action ExitGame;

    public MainMenuScreenController(Action startGame, Action exitGame)
    {
        StartGame = startGame;
        ExitGame = exitGame;
    }
}
