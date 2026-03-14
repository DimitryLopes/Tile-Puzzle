using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour
{
    private const int GRID_SIZE = 3; // 3x3

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private PuzzlePiece[] puzzlePieces;
    [SerializeField]
    private RectTransform emptyPieceRect;

    private PuzzlePiece EmptyPiece => puzzlePieces[puzzlePieces.Length - 1];

    private void Start()
    {
        UpdateInteractables();

        foreach (var piece in puzzlePieces)
        {
            piece.SetIndex();
            Sprite sprite = AssetService.GetPuzzleSprite("Gift", piece.Index);
            piece.Setup(sprite, canvas, OnPieceEndDrag);
        }

        EmptyPiece.SetAsEmpty();
    }

    public void OnGameStarted(string puzzleName)
    {
        UpdateInteractables();

        foreach (var piece in puzzlePieces)
        {
            piece.SetIndex();
            Sprite sprite = AssetService.GetPuzzleSprite(puzzleName, piece.Index);
            piece.Setup(sprite, canvas, OnPieceEndDrag);
        }
    }

    private void UpdateInteractables()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool isAdjacent = IsAdjacent(i, EmptyPiece.Index);
            puzzlePieces[i].SetInteractable(isAdjacent);
        }
    }

    private bool IsAdjacent(int indexA, int indexB)
    {
        int rowA = indexA / GRID_SIZE;
        int colA = indexA % GRID_SIZE;
        int rowB = indexB / GRID_SIZE;
        int colB = indexB % GRID_SIZE;

        return (Mathf.Abs(rowA - rowB) == 1 && colA == colB) ||
               (Mathf.Abs(colA - colB) == 1 && rowA == rowB);
    }

    private void MovePiece(int from, int to)
    {
        var pieceToMove = puzzlePieces[from];
        var targetPiece = puzzlePieces[to];

        pieceToMove.Move(targetPiece);
    }

    private void OnPieceEndDrag(PuzzlePiece piece, PointerEventData data)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            emptyPieceRect,
            data.position,
            data.pressEventCamera,
            out localMousePosition
        ); 
        
        if (emptyPieceRect.rect.Contains(localMousePosition))
        {
            MovePiece(piece.Index, EmptyPiece.Index);
        }
    }
}
