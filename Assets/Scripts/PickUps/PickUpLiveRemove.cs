using System;
using UnityEngine;

public class PickUpLiveRemove : BasePickUp
{

    #region Events

    public static event Action OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        Debug.Log("FFFFFFF");
        OnCapture?.Invoke();
    }

    #endregion
}
