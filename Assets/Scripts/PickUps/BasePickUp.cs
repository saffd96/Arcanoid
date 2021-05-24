using System;
using UnityEngine;

public abstract class BasePickUp : MonoBehaviour
{
    #region Variables

    protected int Score;

    #endregion


    #region Events

    public static event Action<int> OnDestroyed;

    #endregion


    #region Unity Lifecycle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Pad))
        {
            ApplyEffect();
            Destroy(gameObject);
            OnDestroyed?.Invoke(Score);
        }
    }

    #endregion


    #region Private Methods

    protected abstract void ApplyEffect();

    #endregion
}
