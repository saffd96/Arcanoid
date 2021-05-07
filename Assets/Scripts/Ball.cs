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
    [SerializeField] private Vector2 direction;
    [SerializeField] private Transform padTransform;
    [Space]
    [SerializeField] private float ballOffset; //смещение мяча относительно Пада по оси У

    private float ballYPosition;
    private bool isStarted;
    
    public static event Action OnBottomWallCollision;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        GetDirection();
    }

    private void Start()
    {
        ballYPosition = padTransform.position.y + ballOffset;

        if (GameManager.Instance.IsAutoPlay)
        {
            GetDirection();
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
            OnBottomWallCollision?.Invoke();
            isStarted = false;
        }
    }

    #endregion


    #region Privarte Methods

    private void StartBall()
    {
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
        GetDirection();
        rb.velocity = Vector3.zero;
        StartBall();
    }
    #endregion
}
