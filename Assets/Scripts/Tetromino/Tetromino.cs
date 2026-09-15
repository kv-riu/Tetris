using UnityEngine;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S, 
    Z,
}

[System.Serializable]

public enum TetrominoRotation
{
    Spawn = 0,
    Right = 1,
    Reverse = 2,
    Left = 3
}
