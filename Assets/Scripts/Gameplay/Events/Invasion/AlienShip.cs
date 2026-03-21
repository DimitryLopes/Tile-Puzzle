using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AlienShip : Activateable
{
    [SerializeField]
    public RectTransform rectTransform;
    [SerializeField]
    private Button button;
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private float shipSize = 1f;
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private float animationDistance;

    private Action onTargetReached;
    private Action onShipClicked;
    private Vector3 targetPosition;

    private void Awake()
    {
        button.onClick.AddListener(OnPointerClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Setup(Vector3 target, Action onTargetReached, Action onShipDestroyed)
    {
        this.onTargetReached = onTargetReached;
        onShipClicked = onShipDestroyed;
        targetPosition = target;
    }

    public override void OnActivate()
    {
        base.OnActivate();
        rectTransform.rotation = Quaternion.identity;
    }

    public void UpdateShip()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            speed * Time.deltaTime
        );

        var distance = Vector3.Distance(rectTransform.anchoredPosition, targetPosition);
        if (distance < shipSize)
        {
            onTargetReached?.Invoke();
        }
    }

    public void DestroyShip()
    {
        Vector2 current = rectTransform.anchoredPosition;

        Vector2 dir = (current - (Vector2)targetPosition).normalized;

        Vector2 finalPosition = current + dir * animationDistance;

        LeanTween.move(rectTransform, finalPosition, animationDuration)
            .setOnComplete(Deactivate);
        AudioManager.Instance.PlaySFX(AudioKey.event_ship_destroyed);
        rectTransform.LeanRotateZ(Constants.Events.DEFAULT_OBJECT_ROTATION, animationDuration);
    }

    public void OnPointerClick()
    {
        onShipClicked?.Invoke();
        DestroyShip();
    }
}

