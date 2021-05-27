using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LivesView))] public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Text scoreLabel;
    [SerializeField] private GameOverView gameOverView;
    [SerializeField] private WonScreen winScreen;
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
        private set => currentLives = value;
    }
    public int MaxLives
    {
        get => maxLives;
    }
    public int TotalScore { get; private set; }

    #endregion


    #region UnityLifecycle

    private void OnEnable()
    {
        Block.OnDestroyed += UpdateScore;
        Ball.OnBottomWallCollided += LiveRemoved;
        LevelManager.OnTheEnd += ShowWinScreen;
        PickUpLive.OnCapture += AddLive;
        PickUpLiveRemove.OnCapture += LiveRemoved;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= UpdateScore;
        Ball.OnBottomWallCollided -= LiveRemoved;
        LevelManager.OnTheEnd -= ShowWinScreen;
        PickUpLive.OnCapture -= AddLive;
        PickUpLiveRemove.OnCapture -= LiveRemoved;
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
            AddLive();
        }
    }

    #endregion


    #region Public methods

    public void ResetGame()
    {
        maxLives = lives;
        currentLives = maxLives;
        TotalScore = totalScore = 0;

        gameOverView.SetUnactive();
        pauseView.SetUnactive();
        livesView.Setup(maxLives);
        scoreLabel.text = $"Score: {TotalScore}";
        Debug.Log("ResetGame");
    }

    #endregion


    #region Events Handlers

    private void LiveRemoved() //почему-то работает только на первой сцене
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
            livesView.RemoveLife(CurrentLives);
        }

        Debug.Log($"{CurrentLives}, {maxLives}");
    }

    public void UpdateScore(int score)
    {
        TotalScore = totalScore += score;
        scoreLabel.text = $"Score: {TotalScore}";
    }

    #endregion


    #region Private Methods

    private void AddLive()
    {
        if (CurrentLives != MaxLives)
        {
            CurrentLives++;
            livesView.AddLive();
        }
    }

    private void ShowEndScreen()
    {
        gameOverView.SetTotalScore(TotalScore);
        gameOverView.SetActive();
    }

    private void ShowWinScreen()
    {
        winScreen.SetTotalScore(TotalScore);
        winScreen.SetActive();
    }

    #endregion
}
