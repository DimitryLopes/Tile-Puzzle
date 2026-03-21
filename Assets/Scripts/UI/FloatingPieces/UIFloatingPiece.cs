using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFloatingPiece : Activateable, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private UIAnimationComponent idleAnimation;
    [SerializeField]
    private UIAnimationComponent gameAnimation;
    [SerializeField]
    private UIAnimationComponent gameModeAnimation;
    [SerializeField]
    private UIAnimationComponent mainMenuAnimation;
    [SerializeField]
    private UIAnimationComponent optionsAnimation;
    [SerializeField]
    private UIAnimationComponent frameAnimation;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Canvas canvas;

    public RectTransform Rect => rectTransform;


    public void Start()
    {
        PlayIdle();
        Activate();
    }

    public void MoveToMainMenu()
    {
        mainMenuAnimation.PlayInAnimations(PlayIdle);
    }

    public void MoveToGameMode()
    {
        gameModeAnimation.PlayInAnimations(PlayIdle);
    }

    private void PlayIdle()
    {
        idleAnimation.PlayInAnimations();
    }

    public void PlayGameAnimation()
    {
        gameAnimation.PlayInAnimations();
    }

    public void MoveToOptions()
    {
        optionsAnimation.PlayInAnimations();
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
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

        transform.position = worldPoint;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        frameAnimation.PlayInAnimations();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        frameAnimation.PlayOutAnimations();
    }
}

