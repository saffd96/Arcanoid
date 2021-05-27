using System;
using UnityEngine;

public class BasePickUp : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject pickUpVFX;
    [SerializeField] private int score;

    #endregion


    #region Unity Lifecycle

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Pad))
        {
            Debug.Log(gameObject.name);
            ApplyEffect();
            Destroy(gameObject);
        }
    }

    #endregion


    #region Private Methods

    protected virtual void ApplyEffect()
    {
        Instantiate(pickUpVFX, transform.position, Quaternion.identity);
        GameManager.Instance.UpdateScore(score);
    }

    #endregion
}
