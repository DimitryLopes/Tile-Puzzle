using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameModeScreenController : ScreenController
{
    public readonly List<EventData> Events;
    public readonly Action<EventID> OnEventToggled;
    public readonly UnityAction OnPlayButtonClicked;
    public readonly UnityAction OnReturnButtonClicked;
    public readonly UnityAction<Board> OnBoardSelected;
    public readonly Board SelectedBoard;

    public  EventData ClassicEventData => Events[0];

    public GameModeScreenController(List<EventData> events, Action<EventID> onEventToggled,
        UnityAction onPlayButtonClicked, UnityAction onReturnButtonClicked, UnityAction<Board> onBoardSelected
        , Board selectedBoard)
    {
        Events = events;
        OnEventToggled = onEventToggled;
        OnPlayButtonClicked = onPlayButtonClicked;
        OnReturnButtonClicked = onReturnButtonClicked;
        OnBoardSelected = onBoardSelected;
        SelectedBoard = selectedBoard;
    }
}
