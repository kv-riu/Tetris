using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Settings")]
    public GameObject gridCellPrefab;
    public int width = 10;
    public int height = 20;
    public float spacing = 0.1f;
    public float cellScale = 2.0f;

    public Transform[,] grid;

    private void Start()
    {
        CreateBoard();
    }

    public void CreateBoard()
    {
        grid = new Transform[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 position = GetWorldPosition(x, y);
                GameObject cell = Instantiate(gridCellPrefab, position, Quaternion.identity, transform);
                cell.transform.localScale = Vector2.one * cellScale;
                cell.layer = 0;
            }
        }
    }

    public void ClearBoard()
    {
        if (grid == null) return;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }
    }
    public Vector2 GetWorldPosition(int x, int y)
    {
        float posX = x * (1 + spacing);
        float posY = y * (1 + spacing);

        posX -= (width - 1) * (1 + spacing) * 1.0f / 2.0f;
        posY -= (height - 1) * (1 + spacing) * 1.0f / 2.0f;

        return new Vector2(posX, posY);
    }

    public bool IsValidPosition(Vector2Int position, Vector2Int[] cells)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector2Int cellPosition = position + cells[i];

            if (cellPosition.x < 0 || cellPosition.x >= width || cellPosition.y < 0)
            {
                return false;
            }

            if (cellPosition.y < height && grid[cellPosition.x, cellPosition.y] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void SetPieceOnGrid(Piece piece)
    {
        Vector2Int[] cells = piece.GetCurrentPositionData();
        for(int i = 0; i < cells.Length; i++)
        {
            Vector2Int cellPosition = piece.position + cells[i];
            if(cellPosition.x >= 0 && cellPosition.x < width && cellPosition.y >= 0 && cellPosition.y < height)
            {
                Transform blockTransform = piece.blockInstances[i].transform; //transform of the block instance (including sprite)
                blockTransform.SetParent(this.transform);                     //set the parent of the block instance to the board
                grid[cellPosition.x, cellPosition.y] = blockTransform;        //set the grid cell to the block instance transform
            }
        }
        Destroy(piece.gameObject);
        int linesCleared = ClearFullLines();
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnPieceLocked(linesCleared);
        }
    }
    #region Clear Full Lines 
    public int ClearFullLines()
    {
        int cnt = 0;
        for(int y = 0; y < height; y++)
        {
            if (isLineFull(y))
            {
                cnt++;
                ClearLine(y);
                MoveLinesDown(y + 1);
                y--; // Check the same line again after moving lines down
            }
        }
        return cnt;
    }

    private bool isLineFull(int y)
    {
        for(int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void ClearLine(int y)
    {
        for(int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    private void MoveLinesDown(int startY)
    {
        for(int y = startY; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if (grid[x,y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].position = GetWorldPosition(x, y - 1);
                }
            }
        }
    }
    #endregion


}