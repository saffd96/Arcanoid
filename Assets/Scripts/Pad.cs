using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [SerializeField] private float boundary;

    private Ball ball;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            LockPad();
        }
        else
        {
            if (GameManager.Instance.IsAutoPlay)
            {
                Vector3 padPosition = ball.transform.position;
                padPosition.y = transform.position.y;

                padPosition.x = Mathf.Clamp(padPosition.x, -boundary, boundary);

                transform.position = padPosition;
            }
            else
            {
                Vector3 positionInPixels = Input.mousePosition;
                Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

                Vector3 padPosition = positionInWorld;

                padPosition.y = transform.position.y;
                padPosition.z = 0;

                padPosition.x = Mathf.Clamp(padPosition.x, -boundary, boundary);

                transform.position = padPosition;
            }
        }
    }

    #endregion


    #region PrivateRegions

    private void LockPad()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            Vector3 padPosition = transform.position;
        }
    }

    #endregion
}
