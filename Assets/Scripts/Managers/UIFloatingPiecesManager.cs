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

    private UIFloatingPiece lastPiece => floatingPieces[^1];

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
        lastPiece.SetAsLast(OnPieceFinishMoving);
    }

    private void OnScreenAfterShow(IScreen screen)
    {
        Type type = screen.GetType();
        switch (type)
        {
            case Type t when t == typeof(MainMenuScreen):
                foreach(var piece in floatingPieces)
                {
                    piece.MoveToMainMenu();
                }
                break;
            case Type t when t == typeof(GameModeScreen):
                foreach(var piece in floatingPieces)
                {
                    piece.MoveToGameMode();
                }
                break;
            case Type t when t == typeof(GameScreen):
                for(int i = 0; i < floatingPieces.Count; i++)
                {
                    if(i == floatingPieces.Count - 1)
                    {
                        floatingPieces[i].PlayGameAnimation(OnPieceFinishMoving);
                        break;
                    }
                    floatingPieces[i].PlayGameAnimation();
                }
                break;
        }
    }

    private void OnPieceFinishMoving()
    {
        EventManager.OnFloatingPiecesAnimationFinished.Invoke();
    }
}

