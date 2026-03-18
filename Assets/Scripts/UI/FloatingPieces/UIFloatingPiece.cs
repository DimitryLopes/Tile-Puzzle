using System;
using System.Collections;
using UnityEngine;

public class UIFloatingPiece : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Vector2 mainMenuPosition;
    [SerializeField]
    private Vector2 gameModePosition;
    [SerializeField]
    private UIAnimationComponent idleAnimation;
    [SerializeField]
    private UIAnimationComponent gameAnimation;
    [SerializeField]
    private float movementSpeed;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private Action onFinishMoving;

    public void Start()
    {
        MoveToMainMenu();
        PlayIdle();
    }

    public void SetAsLast(Action onFinishMoving)
    {
        this.onFinishMoving = onFinishMoving;
    }

    public void MoveToMainMenu()
    {
        Move(mainMenuPosition);
    }

    public void MoveToGameMode()
    {
        Move(gameModePosition);
    }

    private void Move(Vector2 position, float delay = 0)
    {
        if (delay > 0)
        {
            StartCoroutine(MoveWithDelay(position, delay));
            return;
        }
        targetPosition = position;
        isMoving = true;
    }

    IEnumerator MoveWithDelay(Vector2 position, float delay)
    {
        targetPosition = position;
        yield return new WaitForSeconds(delay);
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving) return;

        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            movementSpeed * Time.deltaTime
        );

        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 0.01f)
        {
            rectTransform.anchoredPosition = targetPosition;
            isMoving = false;
            onFinishMoving?.Invoke();
        }
    }

    private void PlayIdle()
    {
        idleAnimation.PlayInAnimations();
    }

    public void PlayGameAnimation(Action onFinishAction)
    {
        gameAnimation.PlayInAnimations(onFinishAction);
    }

    public void PlayGameAnimation()
    {
        gameAnimation.PlayInAnimations();
    }
}

