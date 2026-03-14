using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform imageRectTransform;
    [SerializeField]
    private Image puzzleImage;

    public int Index { get; private set; }

    private Canvas canvas;
    private Vector3 startPosition;

    private Action<PuzzlePiece, PointerEventData> onEndDragCallback;

    public void Setup(Sprite sprite, Canvas canvas,Action<PuzzlePiece, PointerEventData> endDragCallback)
    {
        puzzleImage.sprite = sprite;
        this.canvas = canvas;
        startPosition = imageRectTransform.anchoredPosition;
        onEndDragCallback = endDragCallback;
    }

    public void SetAsEmpty()
    {
        puzzleImage.enabled = false;
    }

    public void SetIndex()
    {
        Index = transform.GetSiblingIndex();
    }

    public void Move(PuzzlePiece target)
    {
        transform.SetSiblingIndex(target.Index);
        target.transform.SetSiblingIndex(Index);
        SetIndex();
        target.SetIndex();
    }

    public void SetInteractable(bool value)
    {
        puzzleImage.raycastTarget = value;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out worldPoint
        );

        imageRectTransform.position = worldPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDragCallback.Invoke(this, eventData);
        MoveImage();
    }

    private void MoveImage()
    {
        //Tween the image back to its original position, achored position should always 
        //return it to the correct place in the grid, even if the piece has been moved to a different sibling index
        imageRectTransform.anchoredPosition = startPosition;
    }
}
