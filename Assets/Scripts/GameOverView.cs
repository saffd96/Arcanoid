﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    #region Variables

    [SerializeField] private Text gameOverText;
    [SerializeField] private GameManager gameManager;

    #endregion
    
    
    #region Public methods

    public void SetUnactive()
    {
        gameObject.SetActive(false);
    }
    
    public void SetTotalScore(int totalScore)
    {
        gameOverText.text = $"Congrats\nYour score: {totalScore}";
    }

    #endregion


    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}