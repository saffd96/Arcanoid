using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numberOfHits;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Text scoreText;

    private int health;
    private static int score;
    private SpriteRenderer spriteRenderer;

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
        scoreText.text = $"Score: {score}";
    }

    #endregion
}
