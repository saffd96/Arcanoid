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

    #endregion
}
