using System;
using UnityEngine;

public class PickUpLive : BasePickUp
{
    #region Private Methods

    protected override void ApplyEffect()
    {
        GameManager.Instance.AddLive();
    }

    #endregion
}
