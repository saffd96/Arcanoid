using System;
using UnityEngine;

public class PickUpSpeed : BasePickUp
{
    #region Variables

    [SerializeField] private float speedFactor;

    #endregion


    #region Events

    public static event Action<float> OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        OnCapture?.Invoke(speedFactor);
    }

    #endregion
}
