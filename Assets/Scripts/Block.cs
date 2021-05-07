using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numberOfHits;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool isDestroyable;

    private int blockCount;
    private int blockHealth;
    private int score;

    private SpriteRenderer spriteRenderer;

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
        if (isDestroyable)
        {
            OnCreated?.Invoke();
        }

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyable)
        {
            blockHealth--;
            blockCount--;

            if (blockCount == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (blockHealth > 0)
            {
                UpdateSprite();

                return;
            }

            Destroy(gameObject);
            UpdateScore();
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
        OnDestroyed?.Invoke(score);
    }

    #endregion
}
