using System;
using UnityEngine;

public class PirateShip : Activateable
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float shipSize;
    [SerializeField]
    private float animationDistance;
    [SerializeField]
    private float animationDuration;

    public RectTransform RectTransform => rectTransform;

    private Action onTargetReached;
    private TreasureChest chest;

    public void Setup(TreasureChest chest, Action onChestReached)
    {
        onTargetReached = onChestReached;
        this.chest = chest;
        transform.rotation = Quaternion.identity;
    }

    public void UpdateShip()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            Vector2.zero,
            speed * Time.deltaTime
        );

        var distance = Vector3.Distance(rectTransform.anchoredPosition, Vector2.zero);
        if (distance < shipSize)
        {
            onTargetReached?.Invoke();
        }
    }

    public void DestroyShip(Action onMovementFinish)
    {
        onMovementFinish += Deactivate;
        Vector2 current = rectTransform.anchoredPosition;

        Vector2 dir = (chest.RectTransform.anchoredPosition - current).normalized;

        Vector2 finalPosition = current + dir * animationDistance;

        LeanTween.move(rectTransform, finalPosition, animationDuration)
            .setOnComplete(onMovementFinish);

        rectTransform.LeanRotateZ(-Constants.Events.DEFAULT_OBJECT_ROTATION, animationDuration);
    }
}
