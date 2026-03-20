using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFloatingPiecesManager : MonoBehaviour
{
    [SerializeField]
    private List<UIFloatingPiece> floatingPieces;
    [SerializeField]
    private float pieceMoveDelay;

    public static UIFloatingPiecesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EventManager.OnScreenAfterShowEvent.AddListener(OnScreenAfterShow);
        EventManager.OnPuzzleShuffled.AddListener(OnPuzzleShuffled);
        EventManager.OnGameOver.AddListener(OnGameOver);
        EventManager.OnBoardSelected.AddListener(OnBoardSelected);
    }

    private void OnScreenAfterShow(IScreen screen)
    {
        Type type = screen.GetType();
        switch (type)
        {
            case Type t when t == typeof(MainMenuScreen):
                foreach (var piece in floatingPieces)
                {
                    piece.MoveToMainMenu();
                }
                break;
            case Type t when t == typeof(GameModeScreen):
                foreach (var piece in floatingPieces)
                {
                    piece.MoveToGameMode();
                }
                break;
            case Type t when t == typeof(GameScreen):
                foreach (var piece in floatingPieces)
                {
                    piece.PlayGameAnimation();
                }
                break;
        }
    }

    private void OnPieceFinishMoving()
    {
        foreach(var piece in floatingPieces)
        {
            piece.Deactivate();
        }
        EventManager.OnFloatingPiecesAnimationFinished.Invoke();
    }

    public void OnBoardSelected(Board image)
    {
        for (int i = 0; i < floatingPieces.Count; i++)
        {
            var piece = floatingPieces[i];
            var sprite = AssetService.GetPuzzleSprite(image.ToString(), i);
            piece.SetImage(sprite);
        }
    }

    private void OnPuzzleShuffled(PuzzlePiece[] puzzlePieces)
    {
        for(int i = 0; i < floatingPieces.Count; i++)
        {
            var piece = floatingPieces[i];
            var puzzlePiece = puzzlePieces[i];
            var targetPosition = new Vector2(puzzlePiece.RectTransform.anchoredPosition.x - 300,
                puzzlePiece.RectTransform.anchoredPosition.y + 300);

            if (i == floatingPieces.Count - 1)
            {
                LeanTween.move(piece.Rect, targetPosition, pieceMoveDelay * i)
                    .setOnComplete(OnPieceFinishMoving)
                    .setEase(LeanTweenType.easeOutQuart);
                return;
            }
            LeanTween.move(piece.Rect, targetPosition, pieceMoveDelay * i)
                    .setEase(LeanTweenType.easeOutQuart);
        }
    }

    private void OnGameOver(GameEvent evt, bool win)
    {
        foreach(var piece in floatingPieces)
        {
            piece.Activate();
            piece.MoveToMainMenu();
        }
    }
}

