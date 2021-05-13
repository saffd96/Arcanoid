using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }

    public void ResetBlockCount()
    {
        blockCount = 0;
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
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                Debug.Log($"Load Scene {SceneManager.GetActiveScene().buildIndex + 1}");
            }
            else
            {
                OnTheEnd?.Invoke();
            }
        }
    }

    #endregion
}
