using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [SerializeField] private float boundary;

    private Ball ball;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        PickUpPadScale.OnCapture += ChangeScale;
    }

    private void OnDisable()
    {
        PickUpPadScale.OnCapture -= ChangeScale;
    }
    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused)
        {
            return;
        }

        Vector3 padPosition;

        if (GameManager.Instance.IsAutoPlay)
        {
            padPosition = ball.transform.position;
            padPosition.y = transform.position.y;
        }
        else
        {
            Vector3 positionInPixels = Input.mousePosition;
            Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

            padPosition = positionInWorld;

            padPosition.y = transform.position.y;
            padPosition.z = 0;
        }

        padPosition.x = Mathf.Clamp(padPosition.x, -boundary, boundary);
        transform.position = padPosition;
    }

    private void ChangeScale(float scaleFactor)
    {
        var newX = transform.localScale.x * scaleFactor;
        var newScale = new Vector3(newX, transform.localScale.y);
        transform.localScale = newScale;
        Debug.Log("ChangeScale");
    }
    
    #endregion
}
