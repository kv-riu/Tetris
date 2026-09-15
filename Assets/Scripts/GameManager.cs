using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    #region Inspector Fields
    [Header("References")]
    [SerializeField] private Board board;
    [SerializeField] private Spawner spawner;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject startMessage;
    [SerializeField] private GameObject gameOverMessage;
    #endregion

    #region State Properties
    public int Score { get; private set; }
    public int TotalLinesCleared { get; private set; }
    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Auto-find references if missing from Inspector
    }

    private void Start()
    {
        ShowStartScreen();
    }

    private void Update()
    {
        HandleInput();
    }
    #endregion

    #region Input Handling
    private void HandleInput()
    {
        // If the game has not started yet, press Space, Enter, or Click to begin
        if (!IsGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                //Debug.Log("[GameManager] Start input detected -> Starting Game!");
                StartGame();
            }
            return;
        }

        // If the game is over, press Space, Enter, R, or Click to restart
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                //Debug.Log("[GameManager] Restart input detected -> Restarting Game!");
                RestartGame();
            }
            return;
        }

        // While playing, press R to reset and start a fresh session at any time
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("[GameManager] Reset key (R) detected -> Restarting Game!");
            RestartGame();
        }
    }
    #endregion

    #region Game Flow
    /// <summary>
    /// Displays the initial start screen awaiting player input.
    /// </summary>
    public void ShowStartScreen()
    {
        IsGameStarted = false;
        IsGameOver = false;
        UpdateScoreUI(0);

        if (startMessage != null)
        {
            startMessage.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("[GameManager] 'startMessage' is not assigned in Inspector!");
        }

        if (gameOverMessage != null)
        {
            gameOverMessage.SetActive(false);
        }
        else
        {
            //Debug.LogWarning("[GameManager] 'gameOverMessage' is not assigned in Inspector!");
        }

        UpdateScoreUI(0);
        scoreText.SetText("");
    }

    /// <summary>
    /// Starts a new game session.
    /// </summary>
    public void StartGame()
    {
        IsGameStarted = true;
        IsGameOver = false;
        Score = 0;
        TotalLinesCleared = 0;

        if (startMessage != null) startMessage.SetActive(false);
        if (gameOverMessage != null) gameOverMessage.SetActive(false);

        UpdateScoreUI(Score);

        if (spawner != null)
        {
            spawner.SpawnPiece();
        }
        else
        {
            Debug.LogError("[GameManager] 'spawner' reference is missing!");
        }
    }

    /// <summary>
    /// Clears the board, cleans up active pieces, and restarts the game session.
    /// </summary>
    public void RestartGame()
    {
        // 1. Clear all locked blocks on the board
        board.ClearBoard();

        // 2. Destroy the currently falling piece if it exists
        if (spawner.currentPiece != null)
        {
            Destroy(spawner.currentPiece.gameObject);
            spawner.currentPiece = null;
        }

        // 3. Start a fresh game session
        StartGame();
    }

    /// <summary>
    /// Callback triggered by Board whenever a piece locks into place.
    /// </summary>
    /// <param name="linesCleared">Number of full lines cleared in this step.</param>
    public void OnPieceLocked(int linesCleared)
    {
        if (IsGameOver) return;

        if (linesCleared > 0)
        {
            TotalLinesCleared += linesCleared;
            Score += linesCleared * linesCleared * 100;
            UpdateScoreUI(Score);
        }

        spawner.SpawnPiece();
    }

    /// <summary>
    /// Triggers the Game Over state.
    /// </summary>
    public void GameOver()
    {
        IsGameOver = true;

        if (gameOverMessage != null) gameOverMessage.SetActive(true);

        Debug.Log($"Game Over! Final Score: {Score} | Lines Cleared: {TotalLinesCleared}");
    }
    #endregion

    #region Helper Methods
    private void UpdateScoreUI(int currentScore)
    {
        {
            // Use TMPro's zero-allocation formatting method
            scoreText.SetText("Score: {0}", currentScore);
        }
    }

    #endregion
}