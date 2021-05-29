using UnityEngine;

public class PickUpBallScale : BasePickUp
{
    #region Variables

    [SerializeField] private float scaleFactor;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        BallsHandler.Instance.PerformActionWithBalls(ball => ball.ChangeScale(scaleFactor));
    }

    #endregion
}
