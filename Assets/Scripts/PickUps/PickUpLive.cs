using System;
using UnityEngine;

public class PickUpLive : BasePickUp
{
    #region Events

    public static event Action OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        OnCapture?.Invoke();
    }

    #endregion
}
