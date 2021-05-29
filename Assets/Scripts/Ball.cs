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
    [SerializeField] private float ballOffset;

    [Header("Speed")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;

    [Header("For DEV Only")]
    [SerializeField] private Vector2 direction;

    private float ballYPosition;
    private bool isStarted;

    #endregion


    #region Events

    public static event Action<Ball> OnBottomWallCollided;
    public static event Action<Ball> OnCreated;
    public static event Action<Ball> OnDestroyed;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        OnCreated?.Invoke(this);
    }

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

            if (GameManager.Instance.IsAutoPlay || Input.GetMouseButtonDown(0))
            {
                ResetBall();
            }
        }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    #endregion


    #region Public Methods

    public void ChangeSpeed(float speedFactor)
    {
        var newVelosityLenght = Mathf.Clamp(rb.velocity.magnitude * speedFactor, minSpeed, maxSpeed);

        rb.velocity = rb.velocity.normalized * newVelosityLenght;
    }

    public void ChangeScale(float scaleFactor)
    {
        transform.localScale *= scaleFactor;
    }

    public void StartBall()
    {
        GetDirection();
        Vector2 force = direction.normalized * speed;
        rb.velocity = force;
        isStarted = true;
    }

    #endregion


    #region Private Methods

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
