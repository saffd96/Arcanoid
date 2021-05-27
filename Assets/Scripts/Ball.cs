using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D bottomWall;
    [SerializeField] private GameObject ballPrefab;

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

    public static event Action OnBottomWallCollided;

    #endregion


    #region Unity Lifecycle

    private void OnEnable()
    {
        PickUpBallScale.OnCapture += ChangeScale;
        PickUpMultiBall.OnCapture += CreateMultiBalls;
    }

    private void OnDisable()
    {
        PickUpBallScale.OnCapture -= ChangeScale;
        PickUpMultiBall.OnCapture -= CreateMultiBalls;
    }

    private void Start()
    {
        var balls = FindObjectsOfType<Ball>();

        foreach (var ball in balls)
        {
            ballYPosition = padTransform.position.y + ballOffset;

            if (GameManager.Instance.IsAutoPlay)
            {
                StartBall();
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Balls = FindObjectsOfType<Ball>();

        if (collision.collider == bottomWall)
        {
            if (Balls.Length != 1)
            {
                Destroy(gameObject);
            }
            else
            {
                OnBottomWallCollided?.Invoke();
                isStarted = false;
            }
        }
    }

    #endregion


    #region Public Methods

    public void ChangeSpeed(float speedFactor)
    {
        var balls = FindObjectsOfType<Ball>();

        foreach (var ball in balls)
        {
            var newVelosityLenght = Mathf.Clamp(ball.rb.velocity.magnitude * speedFactor, minSpeed, maxSpeed);

            ball.rb.velocity = ball.rb.velocity.normalized * newVelosityLenght;
        }
    }

    #endregion


    #region Private Methods

    private void StartBall()
    {
        GetDirection();
        Vector2 force = direction.normalized * speed;
        rb.velocity = force;
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

    private void ChangeScale(float scaleFactor)
    {
        var balls = FindObjectsOfType<Ball>();

        foreach (var ball in balls)
        {
            ball.transform.localScale *= scaleFactor;
        }
    }

    private void CreateMultiBalls(int ballsCount) //создается череcчур много во время поднятия 2-ого пикапа
    {
        var balls = FindObjectsOfType<Ball>();

        foreach (var ball in balls)
        {
            for (int i = 0; i < ballsCount; i++)
            {
                GameObject spawnedball =
                        Instantiate(ballPrefab, ball.transform.position,
                            Quaternion.identity); // почему не могу создать как Ball?

                var ballComponent = spawnedball.GetComponent<Ball>();

                ballComponent.StartBall();
            }
        }
    }

    #endregion
}
