using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMultiBall : BasePickUp
{
    #region Variables

    [SerializeField] private GameObject pickUpVFX;
    [SerializeField] private int pickUpPoints;
    [SerializeField] private int additionaBalls;

    #endregion


    #region Unity Lifecycle

    #region Events

    public static event Action<int> OnCapture;

    #endregion


    private void Start()
    {
        Score = pickUpPoints;
    }

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        OnCapture?.Invoke(additionaBalls);
    }

    #endregion
}
