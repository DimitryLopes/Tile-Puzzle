using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScreen : UIScreen<GameModeScreenController>
{
    [SerializeField]
    private UIGameEventSelection gameSelectionPrefab;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button returnButton;
    [SerializeField]
    private Transform gameSelectionContainer;

    private List<UIGameEventSelection> eventSelections = new List<UIGameEventSelection>();

    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        foreach(var eventData in Controller.Events)
        {
            var selection = GetAvailableSelection();
            selection.Setup(eventData, Controller.OnEventToggled);
        }

        playButton.onClick.AddListener(Controller.OnPlayButtonClicked);
        returnButton.onClick.AddListener(Controller.OnReturnButtonClicked);
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
        return newSelection;
    }

}
