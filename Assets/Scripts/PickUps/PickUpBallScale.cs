using System;
using UnityEngine;

public class PickUpBallScale : BasePickUp
{
    #region Variables

    [SerializeField] private float scaleFactor;
    
    #endregion



    #region Events

    public static event Action<float> OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        OnCapture?.Invoke(scaleFactor);
    }

    #endregion
}
