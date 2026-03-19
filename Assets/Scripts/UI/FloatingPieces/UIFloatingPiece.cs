using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFloatingPiece : Activateable
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
    private Image image;

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

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}

