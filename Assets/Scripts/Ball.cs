using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;

    private bool isStarted;

    private void Update()
    {
        if (isStarted && Input.GetMouseButtonDown(0))
        {
            StartBall();
        }
    }

    private void StartBall()
    {
        Vector2 force = new Vector2(1, 1) * speed;
        rb.AddForce(force);
        isStarted = true;
    }

}
