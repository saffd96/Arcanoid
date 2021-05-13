using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Text scoreLabel;
    [SerializeField] private GameOverView gameOverView;
    [SerializeField] private LivesView livesView;
    [SerializeField] private PauseView pauseView;

    [Header("SET LIVES!!")]
    [SerializeField] private int lives;

    [Header("Auto play")]
    [SerializeField] private bool isAutoPlay;

    [Header("For DEV")]
    [SerializeField] private int totalScore;
    [SerializeField] private int maxLives;
    [SerializeField] private int currentLives;

    private bool isGamePaused;
    private bool isGameEnded;

    #endregion


    #region Properties

    public bool IsGamePaused
    {
        get => isGamePaused;
        set => isGamePaused = value;
    }
    public bool IsAutoPlay => isAutoPlay;
    public int CurrentLives
    {
        get => currentLives;
        set => currentLives = value;
    }

    public int MaxLives
    {
        get => maxLives;
        set => maxLives = value;
    }

    public int TotalScore { get; private set; }

    #endregion


    #region UnityLifecycle

    private void OnEnable()
    {
        Block.OnDestroyed += OnBlockDestroyed;
        Ball.OnBottomWallCollided += BottomWallCollided;
        LevelManager.OnTheEnd += ShowEndScreen;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= OnBlockDestroyed;
        Ball.OnBottomWallCollided -= BottomWallCollided;
        LevelManager.OnTheEnd -= ShowEndScreen;
    }

    private void Start()
    {
        ResetGame();
        Debug.Log($"{CurrentLives}, {maxLives}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameEnded)
        {
            pauseView.PauseToggle();
        }

        if (Input.GetKeyDown(KeyCode.F) && !isGameEnded)
        {
            if (CurrentLives != MaxLives)
            {
                livesView.AddLive();
                Debug.Log("F Pressed");
            }
        }
    }

    #endregion


    public void ResetGame()
    {
        maxLives = lives;
        Debug.Log(maxLives);
        currentLives = maxLives;
        TotalScore = totalScore = 0;
        

        gameOverView.SetUnactive();
        pauseView.SetUnactive();
        livesView.CreateLivesImages();
        scoreLabel.text = $"Score: {TotalScore.ToString()}";
        Debug.Log("Game resetted");
    }


    #region Events Handlers

    private void BottomWallCollided()
    {
        if (CurrentLives == 1)
        {
            ShowEndScreen();
            Time.timeScale = 0f;
            isGameEnded = true;
        }
        else
        {
            CurrentLives--;
        }

        Debug.Log($"{CurrentLives}, {maxLives}");
    }

    private void OnBlockDestroyed(int score)
    {
        TotalScore = totalScore += score;
        scoreLabel.text = $"Score: {TotalScore.ToString()}";
        Debug.Log("Block Destroyed");
    }

    #endregion


    #region Private Methods

    private void ShowEndScreen()
    {
        gameOverView.SetTotalScore(TotalScore);
        gameOverView.SetActive();
    }

    #endregion
}
