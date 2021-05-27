using UnityEngine;

public class PickUpSpeed : BasePickUp
{
    #region Variables

    [SerializeField] private float speedFactor;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        var balls = FindObjectsOfType<Ball>();

        foreach (var ball in balls)
        {
            ball.ChangeSpeed(speedFactor);
        }
    }

    #endregion
}
