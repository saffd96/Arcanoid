using System;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    #region Variables

    [SerializeField] private int blockCount;

    #endregion


    #region Events

    public static event Action OnTheEnd;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        Block.OnDestroyed += OnBlockDestroyed;
        Block.OnCreated += OnBlockCreated;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= OnBlockDestroyed;
        Block.OnCreated -= OnBlockCreated;
    }

    #endregion


    #region Public methods

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }

    #endregion


    #region Event Handlers

    private void OnBlockCreated()
    {
        blockCount++;
        Debug.Log(blockCount);

    }

    private void OnBlockDestroyed(int score)
    {
        blockCount--;
        Debug.Log(blockCount);

        if (blockCount == 0)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1 < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                OnTheEnd?.Invoke();
            }
        }
    }

    #endregion


    #region Public methods

    public void ResetBlockCount()
    {
        blockCount = 0;
    }

    #endregion
}
