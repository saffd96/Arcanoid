using UnityEngine;

public class PickUpPadScale : BasePickUp
{
    #region Variables

    [SerializeField] private float scaleFactor;

    #endregion


    #region Private Methods

    protected override void ApplyEffect()
    {
        var pad = FindObjectOfType<Pad>();
        pad.ChangeScale(scaleFactor);
    }

    #endregion
}
