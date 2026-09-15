using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Reference")]
    public Board board;
    public Piece piecePrefab;

    [Header("Spawn Settings")]
    public Vector2Int spawnPosition = new Vector2Int(4, 18);

    [HideInInspector] public Piece currentPiece;

    public void SpawnPiece()
    {
        TetrominoDataSO tetrominoData = TetrominoLibrary.Instance.GetRandom();
        if (tetrominoData == null) return;

        //lose condition
        if (!board.IsValidPosition(spawnPosition, tetrominoData.rotationData[0].cells))
        {
            GameManager.Instance.GameOver();
            return;
        }
        //new piece
        GameObject pieceObj = new GameObject("PieceInScene"); // "Create empty" -> rename to "PieceInScene"
        Piece piece = pieceObj.AddComponent<Piece>(); // Add "Piece.cs" script to the new GameObject  
        piece.init(board, spawnPosition, tetrominoData); // Initialize the piece with the board, spawn position
                                                         // , and tetromino data (and init 4 blocks inside)
        currentPiece = piece;
    }
}
