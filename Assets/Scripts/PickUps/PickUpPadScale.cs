using System;
using UnityEngine;

public class PickUpPadScale : BasePickUp
{
    #region Variables

    [SerializeField] private GameObject pickUpVFX;
    [SerializeField] private float scaleFactor;
    [SerializeField] private int pickUpPoints;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        Score = pickUpPoints;
    }

    #endregion


    #region Events

    public static event Action<float> OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        OnCapture?.Invoke(scaleFactor);
    }

    #endregion
}
