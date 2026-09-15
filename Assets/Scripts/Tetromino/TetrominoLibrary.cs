using UnityEngine;
using System.Collections.Generic;
public class TetrominoLibrary : MonoBehaviour
{
    public static TetrominoLibrary Instance { get; private set; }

    [Header("Tetromino Data Assets")]
    public TetrominoDataSO[] tetrominos;

    private Dictionary<Tetromino, TetrominoDataSO> dictionary;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Create dictionary
        dictionary = new Dictionary<Tetromino, TetrominoDataSO>();
        foreach (var tetrominoData in tetrominos)
        {
            dictionary.Add(tetrominoData.tetromino, tetrominoData);
        }
    }

    //Get TetrominoDataSO by Tetromino enum
    public TetrominoDataSO Get(Tetromino tetromino)
    {
        if(dictionary.TryGetValue(tetromino, out TetrominoDataSO tetrominoData))
        {
            return tetrominoData;
        }
        else
        {
            Debug.LogError($"Tetromino {tetromino} not found in library.");
            return null;
        }
    }

    //Get random TetrominoDataSO
    public TetrominoDataSO GetRandom()
    {
        if (tetrominos == null || tetrominos.Length == 0) return null;
        int randomIndex = Random.Range(0, tetrominos.Length);
        return tetrominos[randomIndex];
    }
}
