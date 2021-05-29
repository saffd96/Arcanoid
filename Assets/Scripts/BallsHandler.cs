using System;
using System.Collections.Generic;

public class BallsHandler : SingletonMonoBehaviour<BallsHandler>
{
    private List<Ball> balls = new List<Ball>();


    #region Unity Lifecycle

    private void OnEnable()
    {
        Ball.OnCreated += Ball_OnCreated;
        Ball.OnCreated += Ball_OnDestroyed;
        Ball.OnBottomWallCollided += Ball_OnDestroyed;
    }

    private void OnDisable()
    {
        Ball.OnCreated -= Ball_OnCreated;
        Ball.OnCreated -= Ball_OnDestroyed;
        Ball.OnBottomWallCollided -= Ball_OnDestroyed;

    }

    #endregion


    #region Public Methods

    public void PerformActionWithBalls(Action<Ball> action)
    {
        foreach (var ball in balls)
        {
            action?.Invoke(ball);
        }
    }

    #endregion


    #region Events Handlers

    private void Ball_OnDestroyed(Ball ball)
    {
        balls.Remove(ball);
    }

    private void Ball_OnCreated(Ball ball)
    {
        balls.Add(ball);
    }

    #endregion
}
