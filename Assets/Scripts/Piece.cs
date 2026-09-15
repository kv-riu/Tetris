using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
// This class represents a single piece in the game. It is attached to a GameObject that represents the piece in the scene.
public class Piece : MonoBehaviour
{
    [Header("Data & References")]
    public TetrominoDataSO tetrominoData;
    public Board board;

    [Header("Runtime State")]
    public Vector2Int position;
    public TetrominoRotation currentRotation = TetrominoRotation.Spawn;

    [Header("Falling Speed")]
    public float stepDelay = 0.8f; // Time to fall each step
    private float stepTime;
    private Coroutine fallCoroutine;

    // Transform array
    public Transform[] blockInstances = new Transform[4];
    //Init
    #region Gameplay
    // Spawner calls this method
    public void init(Board board, Vector2Int spawnPosition, TetrominoDataSO data) 
    {
        this.board = board;
        this.position = spawnPosition;
        this.tetrominoData = data;
        this.currentRotation = TetrominoRotation.Spawn;
        this.stepTime = Time.time + stepDelay;

        // Generate 4 blocks for the piece (with color)
        for(int i = 0; i < 4; i++)
        {
            // Instantiate the block prefab and set its parent to the piece
            GameObject block = Instantiate(tetrominoData.blockPrefab, transform);
            block.transform.localScale = Vector3.one * board.cellScale; // use Vector3 instead of Vector2 to prevent scaling issues;
            blockInstances[i] = block.transform;
        }
        UpdatePosition();
        fallCoroutine = StartCoroutine(AutoFallRoutine());
    }
   
    private void Update()
    {
        if(this.tetrominoData == null || this.board == null)
        {
            return;
        }
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        } 
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Step();
            if (fallCoroutine != null) StopCoroutine(fallCoroutine);
            fallCoroutine = StartCoroutine(AutoFallRoutine());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Rotate(1);
        }
    }
    #endregion

    #region Control
    private void Rotate(int direction)
    {
        TetrominoRotation newRotation = (TetrominoRotation)(((int)currentRotation + direction + 4) % 4);
        string rotateKey = $"{(int)currentRotation}->{(int)newRotation}";
        if(this.tetrominoData.tetromino == Tetromino.I) 
        {
            foreach(var offset in WallKickData.I_WallKicks[rotateKey])
            {
                Vector2Int newPosition = this.position + offset;
                if(board.IsValidPosition(newPosition, tetrominoData.rotationData[(int)newRotation].cells))
                {
                    this.position = newPosition;
                    currentRotation = newRotation;
                    UpdatePosition();
                    return;
                }
            }
        }
        else if(this.tetrominoData.tetromino == Tetromino.O)
        {
            // O tetromino does not rotate, so we can just return
            return;
        }
        else
        {
            foreach (var offset in WallKickData.JLSTZ_WallKicks[rotateKey])
            {
                Vector2Int newPosition = this.position + offset;
                if (board.IsValidPosition(newPosition, tetrominoData.rotationData[(int)newRotation].cells))
                {
                    this.position = newPosition;
                    currentRotation = newRotation;
                    UpdatePosition();
                    return;
                }
            }
        }
    }

    private void Move(Vector2Int direction)
    {
        Vector2Int newPosition = this.position + direction;
        if (board.IsValidPosition(newPosition, tetrominoData.rotationData[(int)currentRotation].cells))
        {
            this.position = newPosition;
            UpdatePosition();
        }
    }

    public void Drop()
    {
        while(board.IsValidPosition(this.position + Vector2Int.down, GetCurrentPositionData()))
        {
            this.position += Vector2Int.down;
        }
        UpdatePosition();
        Lock();
    }

    public void Step()
    {
        if (board.IsValidPosition(this.position + Vector2Int.down, GetCurrentPositionData()))
        {
            this.position += Vector2Int.down;
            UpdatePosition();
        }
        else
        {
            Lock();
        }
    }
    #endregion

    #region Position
    public Vector2Int[] GetCurrentPositionData()
    {
        return tetrominoData.rotationData[(int)currentRotation].cells;
    }

    public void UpdatePosition()
    {
        if(tetrominoData == null || board == null)
        {
            return;
        }
        Vector2Int[] cells = GetCurrentPositionData();
        for(int i = 0; i < cells.Length; i++)
        {
            Vector2Int cellPosition = position + cells[i];
            Vector2 worldPosition = board.GetWorldPosition(cellPosition.x, cellPosition.y);
            blockInstances[i].position = worldPosition;
        }
    }
    private void Lock()
    {
        if(fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
        }
        board.SetPieceOnGrid(this);
    }
    #endregion

    private IEnumerator AutoFallRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(stepDelay); //pause for stepDelay seconds then continue
            Step(); //call Step() to move the piece down
        }
    }

    //Start for test
    //private void Start()
    //{
    //    if (board == null || tetrominoData == null)
    //    {
    //        Debug.LogError("Board or TetrominoDataSO is not assigned in the inspector.");
    //        return;
    //    }
    //    init(board, position, tetrominoData);
    //}
}
