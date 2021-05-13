using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D bottomWall;

    [Header("Ball Settings")]
    [SerializeField] private float speed;
    [SerializeField] private Transform padTransform;
    [Space]
    [SerializeField] private float ballOffset; //смещение мяча относительно Пада по оси У

    [Header("For DEV Only")]
    [SerializeField] private Vector2 direction;

    private float ballYPosition;
    private bool isStarted;

    #endregion


    #region Events

    public static event Action OnBottomWallCollided;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        ballYPosition = padTransform.position.y + ballOffset;

        if (GameManager.Instance.IsAutoPlay)
        {
            StartBall();
        }
    }

    private void Update()
    {
        if (!isStarted)
        {
            Vector3 padPosition = padTransform.position;
            var ballTransform = transform;
            padPosition.y = ballYPosition;
            ballTransform.position = padPosition;

            if (GameManager.Instance.IsAutoPlay)
            {
                ResetBall();
            }

            if (Input.GetMouseButtonDown(0))
            {
                ResetBall();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == bottomWall)
        {
            OnBottomWallCollided?.Invoke();
            isStarted = false;
        }
    }

    #endregion


    #region Private Methods

    private void StartBall()
    {
        GetDirection();
        Vector2 force = direction.normalized * speed;
        rb.AddForce(force);
        isStarted = true;
    }

    private void GetDirection()
    {
        direction = new Vector2(Random.Range(-1f, 1f), 1f);
    }

    private void ResetBall()
    {
        rb.velocity = Vector3.zero;
        StartBall();
    }

    #endregion
}
