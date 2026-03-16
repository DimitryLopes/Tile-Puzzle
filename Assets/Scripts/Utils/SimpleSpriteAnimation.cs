using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    private bool isSprite;
    [SerializeField, ShowIf("isSprite", false)]
    private Image image;
    [SerializeField, ShowIf("isSprite", true)]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SimpleAnimationData animationData;

    private float animationTimer;
    private int currentFrameIndex;

    void Update()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationData.frameDuration)
        {
            animationTimer -= animationData.frameDuration;
            currentFrameIndex++;            

            if (currentFrameIndex >= animationData.sprites.Count)
            {
                currentFrameIndex = 0;
            }

            if (isSprite)
            {
                spriteRenderer.sprite = animationData.sprites[currentFrameIndex];
            }
            else
            {
                image.sprite = animationData.sprites[currentFrameIndex];
            }
        }
    }

    [Serializable]
    public struct SimpleAnimationData
    {
        public List<Sprite> sprites;
        public float frameDuration;
        public Action onAnimationFinish;
    }
}
