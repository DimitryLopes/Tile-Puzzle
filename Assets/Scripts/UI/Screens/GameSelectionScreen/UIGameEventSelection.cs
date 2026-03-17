using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEventSelection : Activateable
{
    [SerializeField]
    private Image eventImage;
    [SerializeField]
    private TextMeshProUGUI eventName;
    [SerializeField]
    private Button checkButton;
    [SerializeField]
    private GameObject checkmark;

    private EventID id;
    private Action<EventID> onButtonClick;

    public void Setup(EventData data, Action<EventID> onClick)
    {
        id = data.eventID;
        eventName.text = data.eventID.ToString();
        eventImage.sprite = data.eventIcon;
        checkButton.onClick.RemoveAllListeners();
        checkButton.onClick.AddListener(OnButtonClick);
        ToggleCheckmark(data.IsActive);
        onButtonClick = onClick;
    }

    private void OnButtonClick()
    {
        onButtonClick?.Invoke(id);
        ToggleCheckmark(!checkmark.gameObject.activeSelf);
    }

    private void ToggleCheckmark(bool toggle)
    {
        checkmark.SetActive(toggle);
    }
}
