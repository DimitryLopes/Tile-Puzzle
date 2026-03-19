using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private PuzzlePiece[] puzzlePieces;
    [SerializeField]
    private RectTransform emptyPieceRect;

    private bool isFirstGame = true;
    private PuzzlePiece EmptyPiece => puzzlePieces[puzzlePieces.Length - 1];

    public void StartGame(string puzzleName, Canvas canvas)
    {
        if (isFirstGame)
        {
            for (int i = 0; i < puzzlePieces.Length; i++)
            {
                puzzlePieces[i].SetDefaultIndex(i);
            }
            isFirstGame = false;
        }

        var shuffledPieces = new PuzzlePiece[puzzlePieces.Length];
        puzzlePieces.CopyTo(shuffledPieces, 0);
        shuffledPieces.Shuffle();

        bool isGameOver = false;

        do
        {
            for (int i = 0; i < shuffledPieces.Length; i++)
            {
                Vector2 position = GetPiecePosition(i);
                (shuffledPieces[i].transform as RectTransform).anchoredPosition = position;
                var piece = shuffledPieces[i];
                piece.SetIndex(i);
                Sprite sprite = AssetService.GetPuzzleSprite(puzzleName, piece.DefaultIndex);
                piece.Setup(sprite, canvas, OnPieceEndDrag);
                piece.ToggleImage(false);
                piece.SetInteractable(false);
            }
            isGameOver = CheckGameOver();
        } while (isGameOver);

        EventManager.OnPuzzleShuffled.Invoke(puzzlePieces);
        EmptyPiece.SetAsEmpty();
    }

    public void ShowPieces()
    {
        foreach(PuzzlePiece piece in puzzlePieces)
        {
            if(piece != EmptyPiece)
                piece.ToggleImage(true);
        }
    }

    private Vector2 GetPiecePosition(int index)
    {
        int col = GetPieceColumn(index);
        int row = GetPieceRow(index);
        var X = Constants.Puzzle.PUZLLE_PIECE_STARTING_POSITION_X + col * Constants.Puzzle.PUZZLE_PIECE_POSITION_OFFSET;
        var Y = Constants.Puzzle.PUZLLE_PIECE_STARTING_POSITION_Y + row * -Constants.Puzzle.PUZZLE_PIECE_POSITION_OFFSET;
        return new Vector2(X, Y);
    }

    public void UpdateInteractables()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool isAdjacent = IsAdjacent(puzzlePieces[i].Index, EmptyPiece.Index);
            puzzlePieces[i].SetInteractable(true); //TODO: change to isAdjacent
        }
    }

    private bool CheckGameOver()
    {
        foreach(PuzzlePiece piece in puzzlePieces)
        {
            if (piece.Index != piece.DefaultIndex)
            {
                return false;
            }
        }

        return true;
    }

    #region utils

    private bool IsAdjacent(int indexA, int indexB)
    {
        int rowA = GetPieceRow(indexA);
        int colA = GetPieceColumn(indexA);
        int rowB = GetPieceRow(indexB);
        int colB = GetPieceColumn(indexB);

        return (Mathf.Abs(rowA - rowB) == 1 && colA == colB) ||
               (Mathf.Abs(colA - colB) == 1 && rowA == rowB);
    }

    private int GetPieceRow(int index)
    {
        return index / Constants.Puzzle.GRID_SIZE;
    }

    private int GetPieceColumn(int index)
    {
        return index % Constants.Puzzle.GRID_SIZE;
    }
    #endregion

    private void OnPieceEndDrag(PuzzlePiece piece, PointerEventData data)
    {
        if (EmptyPiece.IsMouseOver)
        {
            piece.Move(EmptyPiece);
            bool isGameOver = CheckGameOver();

            if (isGameOver)
            {
                Debug.Log("Game Over!");
                return;
            }
            UpdateInteractables();
        }
        else
        {
            piece.ResetImagePosition();
        }
    }
}
