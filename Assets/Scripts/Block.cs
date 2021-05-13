using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numberOfHits;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool isDestroyable;

    private int blockHealth;
    private int score;

    private SpriteRenderer spriteRenderer;

    #endregion


    #region Events

    public static event Action<int> OnDestroyed;
    public static event Action OnCreated;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        blockHealth = numberOfHits;
        score = 0;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();

        if (isDestroyable)
        {
            OnCreated?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyable)
        {
            blockHealth--;

            if (blockHealth > 0)
            {
                UpdateSprite();

                return;
            }

            Destroy(gameObject);
            UpdateScore();
            OnDestroyed?.Invoke(score);

        }
    }

    #endregion


    #region Private Methods

    private void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[blockHealth - 1];
    }

    private void UpdateScore()
    {
        score += numberOfHits * 100;
    }

    #endregion
}
