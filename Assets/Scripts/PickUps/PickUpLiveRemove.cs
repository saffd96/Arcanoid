using System;
using UnityEngine;

public class PickUpLiveRemove : BasePickUp
{
    #region Variables

    [SerializeField] private GameObject pickUpVFX;

    [SerializeField] private int pickUpPoints;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        Score = pickUpPoints;
    }

    #endregion


    #region Events

    public static event Action OnCapture;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        Debug.Log("FFFFFFF");
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        OnCapture?.Invoke();
    }

    #endregion
}
