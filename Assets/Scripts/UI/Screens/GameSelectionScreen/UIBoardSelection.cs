using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBoardSelection : Activateable
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject checkmark;
    [SerializeField]
    private TextMeshProUGUI boardName;
    [SerializeField]
    private Image boardImage;

    private Board board;
    private Action<UIBoardSelection> onSelected;

    public Board Board => board;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    public void Setup(Board board, Action<UIBoardSelection> onSelected)
    {
        string boardName = board.ToString();
        this.board = board;
        this.onSelected = onSelected;
        boardImage.sprite = AssetService.GetBoardSprite(boardName);
        this.boardName.text = boardName;
        ToggleCheckmark(false);
    }

    private void OnButtonClicked()
    {
        onSelected?.Invoke(this);
        if (checkmark.gameObject.activeSelf)
        {
            AudioManager.Instance.PlaySFX(AudioKey.UI_click_1);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioKey.UI_click_2);
        }
    }

    public void ToggleCheckmark(bool toggle)
    {
        checkmark.SetActive(toggle);
    }
}
