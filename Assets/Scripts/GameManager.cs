using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject pauseViewGameObject;
    [SerializeField] private GameObject gameOverViewGameObject;
    [SerializeField] private GameObject livePrefab;

    [Header("Other")]
    [SerializeField] private GameObject canvasGameObject;
    [Header("SET LIVES!!")]
    [SerializeField] private int lives;

    [Header("Auto play")]
    [SerializeField] private bool isAutoPlay;

    [SerializeField] private GameObject[] livesImages;
    private int totalScore;
    private bool isGamePaused;
    private bool isGameEnded;

    #endregion


    #region Properties

    public bool IsGamePaused => isGamePaused;
    public bool IsAutoPlay => isAutoPlay;
    public int Lives
    {
        get => lives;
        set => lives = value;
    }    
    #endregion


    #region UnityLifecycle

    private void Start()
    {
        pauseViewGameObject.SetActive(false);
        gameOverViewGameObject.SetActive(false);

        livesImages = new GameObject[lives];
        totalScore = 0;
        CreateLivesImages();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)&&!isGameEnded)
        {
            PauseToggle();
        }
    }

    private void OnEnable()
    {
        Block.OnDestroyed += OnBlockDestroyed;
        Ball.OnBottomWallCollision += OnBottomWallCollision;
        LevelManager.OnTheEnd += ShowEndScreen;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= OnBlockDestroyed;
        Ball.OnBottomWallCollision -= OnBottomWallCollision;
        LevelManager.OnTheEnd -= ShowEndScreen;
    }

    #endregion


    #region Events Handlers

    private void OnBottomWallCollision()
    {
        if (Lives == 1)
        {
            ShowEndScreen();
            Time.timeScale = 0f;
            isGameEnded = true;
        }
        else
        {
            Lives--;
            Destroy(livesImages[lives-1]);
        }
    }

    private void OnBlockDestroyed(int score)
    {
        totalScore += score;
        scoreLabel.text = $"Score: {totalScore.ToString()}";
    }

    #endregion


    #region Private Methods

    private void PauseToggle()
    {
        isGamePaused = !isGamePaused;

        Time.timeScale = isGamePaused ? 0f : 1f;

        pauseViewGameObject.SetActive(isGamePaused);
    }

    private void ShowEndScreen()
    {
        gameOverText.text = $"Congrats\nYour score: {totalScore}";
        gameOverViewGameObject.SetActive(true);
    }

    private void CreateLivesImages()
    {
        for (int i = 0; i < lives-1; i++)
        {
            livesImages[i] = Instantiate(livePrefab, new Vector3(10f + 60 * i, 10f), Quaternion.identity);
            livesImages[i].transform.SetParent(canvasGameObject.transform, false);
        }
    }

    #endregion


    #region Public Methods

    public void ExitGame()
    {
        Application.Quit();
        Debug.LogError("QUIT");
    }

    public void ContinueGame() //не хотел делать публичным PauseToggle()
    {
        PauseToggle();
        Debug.LogError("Continue");
    }

    #endregion
}
