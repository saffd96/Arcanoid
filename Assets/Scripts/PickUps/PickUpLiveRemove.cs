using System;
using UnityEngine;

public class PickUpLiveRemove : BasePickUp
{
    #region Private Methods

    protected override void ApplyEffect()
    {
        Debug.Log("FFFFFFF");
        GameManager.Instance.RemoveLive();
    }

    #endregion
}
