using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    private PuzzlePiece[] puzzlePieces;
    [SerializeField]
    private RectTransform emptyPieceRect;

    private PuzzlePiece EmptyPiece => puzzlePieces[puzzlePieces.Length - 1];

    public void StartGame(string puzzleName, Canvas canvas)
    {   
        for(int i = 0; i < puzzlePieces.Length; i++)
        {
            Vector2 position = GetPiecePosition(i);
            (puzzlePieces[i].transform as RectTransform).anchoredPosition = position;
            var piece = puzzlePieces[i];
            piece.SetIndex(i);
            Sprite sprite = AssetService.GetPuzzleSprite(puzzleName, piece.Index);
            piece.Setup(sprite, canvas, OnPieceEndDrag);
        }
        EmptyPiece.SetAsEmpty();
        UpdateInteractables();
    }

    private Vector2 GetPiecePosition(int index)
    {
        int col = GetPieceColumn(index);
        int row = GetPieceRow(index);
        var X = Constants.Puzzle.PUZLLE_PIECE_STARTING_POSITION_X + col * Constants.Puzzle.PUZZLE_PIECE_POSITION_OFFSET;
        var Y = Constants.Puzzle.PUZLLE_PIECE_STARTING_POSITION_Y + row * -Constants.Puzzle.PUZZLE_PIECE_POSITION_OFFSET;
        return new Vector2(X, Y);
    }

    private void UpdateInteractables()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool isAdjacent = IsAdjacent(puzzlePieces[i].Index, EmptyPiece.Index);
            puzzlePieces[i].SetInteractable(isAdjacent);
        }
    }

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

    private void OnPieceEndDrag(PuzzlePiece piece, PointerEventData data)
    {
        if (EmptyPiece.IsMouseOver)
        {
            piece.Move(EmptyPiece);
            UpdateInteractables();
        }
        else
        {
            piece.ResetImagePosition();
        }
    }
}
