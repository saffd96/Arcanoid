using System;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numberOfHits;
    [SerializeField] private Sprite[] sprites;

    private int health;
    private int score;
    private SpriteRenderer spriteRenderer;

    public static event Action<int> OnBlockDestroyed;
    
    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        health = numberOfHits;
        score = 0;
    }

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health--;

        if (health > 0)
        {
            UpdateSprite();

            return;
        }

        Destroy(gameObject);
        UpdateScore();
    }

    #endregion


    #region Private Methods

    private void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[health - 1];
    }

    private void UpdateScore()
    {
        score += numberOfHits * 100;
        OnBlockDestroyed?.Invoke(score);
    }

    #endregion
}
