using UnityEngine;

public class PickUpMultiBall : BasePickUp
{
    #region Variables

    [SerializeField] private int additionaBalls = 2;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        BallsHandler.Instance.PerformActionWithBalls(CloneBall);
    }

    #endregion


    #region Private Methods

    private void CloneBall(Ball ball)
    {
        for (int i = 0; i < additionaBalls; i++)
        {
            var newBall = Instantiate(ball, ball.transform.position, ball.transform.rotation);
            newBall.StartBall();
        }
    }

    #endregion
}
