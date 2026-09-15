using UnityEngine;

[CreateAssetMenu(fileName = "TetrominoData", menuName = "Tetris/TetrominosDataSO")]
public class TetrominoDataSO : ScriptableObject
{
    [Header("Tetromino Info")]
    public Tetromino tetromino;
    public GameObject blockPrefab;

    [Header("Tetromino Cells Offset")]
    public RotationData[] rotationData;

    // Auto fill data for each tetromino type
    // 1. Open an asset of this scriptable object in the inspector
    // 2. Click setting icon in the top right corner of the inspector
    // 3. Click the "Set Default Rotations" button in the context menu of the inspector
    [ContextMenu("Set Default Rotations")]
    public void SetDefaultRotations()
    {
        rotationData = new RotationData[4];
        for (int i = 0; i < 4; i++)
        {
            rotationData[i] = new RotationData();
        }

        switch (tetromino)
        {
            case Tetromino.I:
                rotationData[0].cells = new Vector2Int[] { new(-1, 0), new(0, 0), new(1, 0), new(2, 0) };
                rotationData[1].cells = new Vector2Int[] { new(1, 1), new(1, 0), new(1, -1), new(1, -2) };
                rotationData[2].cells = new Vector2Int[] { new(-1, -1), new(0, -1), new(1, -1), new(2, -1) };
                rotationData[3].cells = new Vector2Int[] { new(0, 1), new(0, 0), new(0, -1), new(0, -2) };
                break;

            case Tetromino.O:
                Vector2Int[] oCells = new Vector2Int[] { new(0, 0), new(1, 0), new(0, 1), new(1, 1) };
                for (int i = 0; i < 4; i++) rotationData[i].cells = (Vector2Int[])oCells.Clone();
                break;

            case Tetromino.T:
                rotationData[0].cells = new Vector2Int[] { new(-1, 0), new(0, 0), new(1, 0), new(0, 1) };
                rotationData[1].cells = new Vector2Int[] { new(0, 1), new(0, 0), new(0, -1), new(1, 0) };
                rotationData[2].cells = new Vector2Int[] { new(-1, 0), new(0, 0), new(1, 0), new(0, -1) };
                rotationData[3].cells = new Vector2Int[] { new(0, 1), new(0, 0), new(0, -1), new(-1, 0) };
                break;

            case Tetromino.J:
                rotationData[0].cells = new Vector2Int[] { new(-1, 1), new(-1, 0), new(0, 0), new(1, 0) };
                rotationData[1].cells = new Vector2Int[] { new(1, 1), new(0, 1), new(0, 0), new(0, -1) };
                rotationData[2].cells = new Vector2Int[] { new(1, -1), new(1, 0), new(0, 0), new(-1, 0) };
                rotationData[3].cells = new Vector2Int[] { new(-1, -1), new(0, -1), new(0, 0), new(0, 1) };
                break;

            case Tetromino.L:
                rotationData[0].cells = new Vector2Int[] { new(1, 1), new(-1, 0), new(0, 0), new(1, 0) };
                rotationData[1].cells = new Vector2Int[] { new(1, -1), new(0, 1), new(0, 0), new(0, -1) };
                rotationData[2].cells = new Vector2Int[] { new(-1, -1), new(1, 0), new(0, 0), new(-1, 0) };
                rotationData[3].cells = new Vector2Int[] { new(-1, 1), new(0, -1), new(0, 0), new(0, 1) };
                break;

            case Tetromino.S:
                rotationData[0].cells = new Vector2Int[] { new(-1, 0), new(0, 0), new(0, 1), new(1, 1) };
                rotationData[1].cells = new Vector2Int[] { new(0, 1), new(0, 0), new(1, 0), new(1, -1) };
                rotationData[2].cells = new Vector2Int[] { new(1, 0), new(0, 0), new(0, -1), new(-1, -1) };
                rotationData[3].cells = new Vector2Int[] { new(0, -1), new(0, 0), new(-1, 0), new(-1, 1) };
                break;

            case Tetromino.Z:
                rotationData[0].cells = new Vector2Int[] { new(-1, 1), new(0, 1), new(0, 0), new(1, 0) };
                rotationData[1].cells = new Vector2Int[] { new(1, 1), new(1, 0), new(0, 0), new(0, -1) };
                rotationData[2].cells = new Vector2Int[] { new(1, -1), new(0, -1), new(0, 0), new(-1, 0) };
                rotationData[3].cells = new Vector2Int[] { new(-1, -1), new(-1, 0), new(0, 0), new(0, 1) };
                break;
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}