using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("References")]
    public Board board;
    public Spawner spawner;
    public GhostCell ghostCellPrefab;

    private GhostCell[] cells = new GhostCell[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = Instantiate(ghostCellPrefab, transform);
            cells[i].transform.localScale = Vector3.one * board.cellScale;
        }
    }

    private void LateUpdate()
    {
        if (spawner == null || spawner.currentPiece == null)
        {
            SetVisible(false);
            return;
        }

        SetVisible(true);
        UpdateGhost(spawner.currentPiece);
    }

    private void UpdateGhost(Piece activePiece)
    {
        Vector2Int[] pieceCells = activePiece.GetCurrentPositionData();
        Vector2Int ghostPosition = activePiece.position;

        while (board.IsValidPosition(ghostPosition + Vector2Int.down, pieceCells))
        {
            ghostPosition += Vector2Int.down;
        }

        for (int i = 0; i < 4; i++)
        {
            Vector2Int cellCoord = pieceCells[i];

            Vector2Int gridPos = ghostPosition + cellCoord;
            cells[i].transform.position = board.GetWorldPosition(gridPos.x, gridPos.y);

            bool hasTop = ContainsCell(pieceCells, cellCoord + Vector2Int.up);
            bool hasBottom = ContainsCell(pieceCells, cellCoord + Vector2Int.down);
            bool hasLeft = ContainsCell(pieceCells, cellCoord + Vector2Int.left);
            bool hasRight = ContainsCell(pieceCells, cellCoord + Vector2Int.right);

            cells[i].SetBorders(!hasTop, !hasBottom, !hasLeft, !hasRight);
        }
    }

    private bool ContainsCell(Vector2Int[] allCells, Vector2Int target)
    {
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i] == target) return true;
        }
        return false;
    }

    private void SetVisible(bool visible)
    {
        for (int i = 0; i < 4; i++)
        {
            if (cells[i] != null)
            {
                cells[i].gameObject.SetActive(visible);
            }
        }
    }
}