using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Text scoreLabel;
    [SerializeField] private GameObject pauseVievGameObject;

    [FormerlySerializedAs("Autoplay")]
    [Header("Auto play")]
    [SerializeField] private bool isAutoPlay;

    private int totalScore;
    private bool isGamePaused;

    #endregion


    #region Properties

    public bool IsGamePaused => isGamePaused;
    public bool IsAutoPlay => isAutoPlay;

    #endregion


    #region UnityLifecycle

    private void Start()
    {
        pauseVievGameObject.SetActive(false);
        totalScore = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    private void OnEnable()
    {
        Block.OnDestroyed += OnBlockDestroyed;
    }

    private void OnDisable()
    {
        Block.OnDestroyed -= OnBlockDestroyed;
    }

    #endregion


    #region Events Handlers

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

        pauseVievGameObject.SetActive(isGamePaused);
    }

    #endregion


    #region PublicMethods

    public void ExitGame()
    {
        Application.Quit();
        Debug.LogError("QUIT");
    }

    public void ContinueGame()
    {
        PauseToggle();
        Debug.LogError("Continue");
    }

    #endregion
}
