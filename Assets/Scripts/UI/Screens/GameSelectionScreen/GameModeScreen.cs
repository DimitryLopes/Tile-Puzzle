using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScreen : UIScreen<GameModeScreenController>
{
    [SerializeField]
    private UIBoardSelection boardSelectionPrefab;
    [SerializeField]
    private UIGameEventSelection classicModeSelection;
    [SerializeField]
    private UIGameEventSelection gameSelectionPrefab;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button returnButton;
    [SerializeField]
    private Transform gameSelectionContainer;
    [SerializeField]
    private Transform boardSelectionContainer;

    private List<UIGameEventSelection> eventSelections = new List<UIGameEventSelection>();
    private List<UIBoardSelection> boardSelections = new List<UIBoardSelection>();

    private UIBoardSelection selectedBoard;

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        //skips the first one because it's the classic one
        for (int i = 1; i < Controller.Events.Count; i++)
        {
            var selection = GetAvailableSelection();
            selection.Setup(Controller.Events[i], Controller.OnEventToggled);
        }
        //classic one is a special boy
        classicModeSelection.Setup(Controller.ClassicEventData, OnClassicModeSelected);

        foreach (Board board in System.Enum.GetValues(typeof(Board)))
        {
            var selection = GetAvailableBoardSelection();
            selection.Setup(board, SelectBoard);

            if(board == Controller.SelectedBoard)
            {
                SelectBoard(selection);
            }

        }

        playButton.onClick.AddListener(Controller.OnPlayButtonClicked);
        returnButton.onClick.AddListener(Controller.OnReturnButtonClicked);
    }

    private void OnClassicModeSelected(EventID id = EventID.Classic)
    {
        Controller.OnEventToggled.Invoke(EventID.Classic);

        foreach (var selection in eventSelections)
        {
            selection.ToggleInteraction(!classicModeSelection.IsSelected);
            if (selection.IsSelected)
            {
                selection.ForceToggle();
            }
        }
    }

    private void SelectBoard(UIBoardSelection boardSelection)
    {
        if(selectedBoard != null)
        {
            selectedBoard.ToggleCheckmark(false);
        }

        if(selectedBoard == boardSelection)
        {
            return;
        }

        selectedBoard = boardSelection;
        selectedBoard.ToggleCheckmark(true);
        Controller.OnBoardSelected.Invoke(selectedBoard.Board);
    }

    protected override void OnAfterHide()
    {
        base.OnAfterHide();
        foreach(var selection in eventSelections)
        {
            selection.Deactivate();
        }

        foreach(var selection in boardSelections)
        {
            selection.Deactivate();
        }
    }

    private UIGameEventSelection GetAvailableSelection()
    {
        foreach(var selection in eventSelections)
        {
            if (selection.IsActive) continue;
            selection.Activate();
        }

        var newSelection = Instantiate(gameSelectionPrefab, gameSelectionContainer);
        newSelection.Activate();
        eventSelections.Add(newSelection);
        return newSelection;
    }

    private UIBoardSelection GetAvailableBoardSelection()
    {
        foreach (var selection in boardSelections)
        {
            if (selection.IsActive) continue;
            selection.Activate();
        }

        var newSelection = Instantiate(boardSelectionPrefab, boardSelectionContainer);
        newSelection.Activate();
        boardSelections.Add(newSelection);
        return newSelection;
    }

}
