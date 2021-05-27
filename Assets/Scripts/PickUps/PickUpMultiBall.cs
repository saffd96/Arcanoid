using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMultiBall : BasePickUp
{
    #region Variables

    [SerializeField] private int additionaBalls;

    #endregion



    #region Events

    public static event Action<int> OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        OnCapture?.Invoke(additionaBalls);
    }

    #endregion
}
