using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    #region Variables

    private int blockCount;

    #endregion


    #region PrivateMethods

    private void Start()
    {
        Debug.Log(blockCount); //почему-то показыватся не верное значение. хотя все работает нормально О_О
    }

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


    #region Event Handlers

    private void OnBlockCreated()
    {
        blockCount++;
    }

    private void OnBlockDestroyed(int score)
    {
        blockCount--;
        Debug.Log(blockCount);

        if (blockCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    #endregion
}
