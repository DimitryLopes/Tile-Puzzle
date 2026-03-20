using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image chestImage;
    [SerializeField]
    private float animationDistance = 400f;
    [SerializeField]
    private float animationDuration = 0.5f;

    public int Index { get; private set; }
    public RectTransform RectTransform => rectTransform;

    public void Open(PirateShip pirateShip, Action onFinishMoving)
    {
        Vector2 current = rectTransform.anchoredPosition;

        Vector2 dir = (current - pirateShip.RectTransform.anchoredPosition).normalized;

        Vector2 finalPosition = current + dir * animationDistance;

        LeanTween.move(rectTransform, finalPosition, animationDuration)
            .setOnComplete(onFinishMoving);

        rectTransform.LeanRotateZ(Constants.Events.DEFAULT_OBJECT_ROTATION, animationDuration);
    }

    public void SetupChest(Vector2 position, int index, Transform parent)
    {
        transform.SetParent(parent);
        transform.SetAsFirstSibling();
        Index = index;
        rectTransform.position = position;
        rectTransform.rotation = Quaternion.identity;
    }
}
