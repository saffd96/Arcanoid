﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Transform padTransform;
    [SerializeField] private Collider2D bottomWall;

    private bool isStarted;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        direction = new Vector2(Random.Range(-1f, 1f), 1f);
    }

    private void Start()
    {
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
            var transform1 = transform;
            padPosition.y = transform1.position.y;
            transform1.position = padPosition;

            if (Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, rb.velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == bottomWall)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    #endregion
}
