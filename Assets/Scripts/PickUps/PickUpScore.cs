using UnityEngine;

public class PickUpScore : BasePickUp
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


    #region Private Methods

    protected override void ApplyEffect()
    {
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
    }

    #endregion
}
