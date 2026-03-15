using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlienShip : Activateable, IPointerClickHandler
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float shipSize = 1f;
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private float animationDistance;

    private Action onTargetReached;
    private Action onShipClicked;
    private Vector3 targetPosition;

    public void Setup(Vector3 target, Action onTargetReached, Action onShipDestroyed)
    {
        this.onTargetReached = onTargetReached;
        onShipClicked = onShipDestroyed;
        targetPosition = target;
    }

    private void Update()
    {
        transform.Translate(transform.position - targetPosition * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, targetPosition) < shipSize)
        {
            onTargetReached?.Invoke();
        }
    }
    
    public void DestroyShip()
    {
        var normalizedTarget = (targetPosition - transform.position).normalized;
        transform.LeanMoveLocal(normalizedTarget * animationDistance, animationDuration)
            .setOnComplete(Deactivate);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onShipClicked?.Invoke();
        DestroyShip();
    }
}

