using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PiratesEvent : GameEvent
{
    [SerializeField, Space]
    private TreasureChest treasureChest;
    [SerializeField]
    private PirateShip pirateShip;
    [SerializeField]
    private Vector2 shipSpawnPoint;

    private bool isRunning;

    public override void EndEvent(bool isWin)
    {
        EventManager.OnGameEventEnded.Invoke(this, isWin);
    }

    public override void StartEvent()
    {
        EventManager.OnPieceMoved.AddListener(OnPieceMoved);
        ShuffleChest();
        pirateShip.Setup(treasureChest, OnShipReachedChest);
        PositionShip();
        isRunning = true;
    }

    public override void UpdateEvent()
    {
        pirateShip.UpdateShip();
    }

    private void OnPieceMoved()
    {
        PuzzlePiece piece = GameManager.Instance.Puzzle.EmptyPiece;
        if (piece.Index == treasureChest.Index)
        {
            treasureChest.Open(pirateShip, OnChestOpened);
        }
    }

    private void OnChestOpened()
    {
        isRunning = false;
        pirateShip.DestroyShip(OnShipDestroyed);        
    }

    private void ShuffleChest()
    {
        int randomIndex = Random.Range(0, 8); //board slots
        //this avoids choosing the empty space
        var puzzle = GameManager.Instance.Puzzle;
        PuzzlePiece piece = puzzle.PuzzlePieces[randomIndex];
        treasureChest.SetupChest(piece.RectTransform.position, piece.Index, puzzle.PiecesContainer);
    }

    private void PositionShip()
    {
        pirateShip.Activate();
        pirateShip.RectTransform.anchoredPosition = shipSpawnPoint;
    }

    private void OnShipReachedChest()
    {
        if (!isRunning) return;
        isRunning = false;
        EndEvent(false);
    }

    private void OnShipDestroyed()
    {
        isRunning = false;
        EndEvent(true);
    }
}
