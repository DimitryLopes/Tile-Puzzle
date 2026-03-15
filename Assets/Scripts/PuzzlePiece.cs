using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RectTransform imageRectTransform;
    [SerializeField]
    private Image emptyFrame;
    [SerializeField]
    private Image puzzleImage;
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private UIAnimationComponent puzzleImageAnimation;
    [SerializeField]
    private UIAnimationComponent emptyFrameAnimation;

    public int Index { get; private set; }
    public bool IsMouseOver { get; private set; }

    private bool isEmpty = false;
    private Canvas canvas;

    private Action<PuzzlePiece, PointerEventData> onEndDragCallback;

    public void Setup(Sprite sprite, Canvas canvas, Action<PuzzlePiece, PointerEventData> endDragCallback)
    {
        puzzleImage.sprite = sprite;
        this.canvas = canvas;
        onEndDragCallback = endDragCallback;
        isEmpty = false;
    }

    public void SetAsEmpty()
    {
        puzzleImage.gameObject.SetActive(false);
        emptyFrame.gameObject.SetActive(true);
        isEmpty = true;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public void Move(PuzzlePiece target)
    {
        RectTransform rect = transform as RectTransform;
        RectTransform targetRect = target.transform as RectTransform;

        Vector2 holderPosition = rect.anchoredPosition;
        Vector2 targetPosition = targetRect.anchoredPosition;

        LeanTween.move(rect, targetPosition, animationDuration)
            .setEase(LeanTweenType.easeOutQuart);

        LeanTween.move(targetRect, holderPosition, animationDuration)
            .setEase(LeanTweenType.easeOutQuart);

        var indexHolder = Index;
        Index = target.Index;
        target.SetIndex(indexHolder);

        ResetImagePosition();
    }

    public void ResetImagePosition()
    {
        puzzleImageAnimation.PlayInAnimations();
    }

    public void SetImagePosition(Vector3 position)
    {
        imageRectTransform.position = position;
    }

    public void SetInteractable(bool value)
    {
        puzzleImage.raycastTarget = value;
    }

    private void UpdateEmptyFrame()
    {
        if (!isEmpty) return;

        if (IsMouseOver)
        {
            emptyFrameAnimation.PlayInAnimations();
        }
        else
        {
            emptyFrameAnimation.PlayOutAnimations();
        }
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsMouseOver = true;
        UpdateEmptyFrame();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsMouseOver = false;
        UpdateEmptyFrame();
    }

}

