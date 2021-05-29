using UnityEngine;

public class PickUpSpeed : BasePickUp
{
    #region Variables

    [SerializeField] private float speedFactor;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        BallsHandler.Instance.PerformActionWithBalls(ball => ball.ChangeSpeed(speedFactor));
    }

    #endregion
}
