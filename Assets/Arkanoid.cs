using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arkanoid : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;

    private int totalScore;

    private void OnEnable()
    {
        Block.OnBlockDestroyed += Block_Ondestroyed;
    }

    private void OnDisable()
    {
        Block.OnBlockDestroyed -= Block_Ondestroyed;
    }

    private void Block_Ondestroyed(int score)
    {
        totalScore += score;
        scoreLabel.text = $"Score: {totalScore.ToString()}";
    }
}
