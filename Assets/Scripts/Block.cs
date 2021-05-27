using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    #region Variables

    [Header("Base settings")]
    [SerializeField] private int numberOfHits;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool isDestroyable;

    [Header("PickUp Settings")]
    [SerializeField] private GameObject[] pickUpPrefabs;

    [Range(1, 100)]
    [SerializeField] private int chanceToDrop = 10;

    [Header("Vfx")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject destroyBlockParticlePrefab;

    [Header("Explosive block")]
    [SerializeField] private bool isExplosive;
    [SerializeField] private float explosiveRadius;

    private int blockHealth;
    private int score;

    private SpriteRenderer spriteRenderer;

    #endregion


    #region Events

    public static event Action<int> OnDestroyed;
    public static event Action<Vector3> OnDestroyedPosition;

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
        var contact = collision.GetContact(0);

        Instantiate(hitEffect, contact.point, Quaternion.identity);

        DestroyBlock();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }

    #endregion


    #region Private Methods

    private void DestroyBlock()
    {
        if (!isDestroyable)
        {
            return;
        }

        if (IsNeedToCreatePickUp())
        {
            int index = Random.Range(0, pickUpPrefabs.Length);
            Instantiate(pickUpPrefabs[index], transform.position, Quaternion.identity);
        }

        blockHealth--;

        if (blockHealth > 0)
        {
            UpdateSprite();

            return;
        }

        Instantiate(destroyBlockParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        UpdateScore();
        Explode();

        OnDestroyed?.Invoke(score);
        OnDestroyedPosition?.Invoke(transform.position);
    }

    private void Explode()
    {
        if (!isExplosive)
        {
            return;
        }

        LayerMask mask = LayerMask.GetMask(Masks.Block);
        var objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explosiveRadius, mask);

        foreach (var objInRadius in objectsInRadius)
        {
            var block = objInRadius.gameObject.GetComponent<Block>();

            if (block == null)
            {
                Destroy(objInRadius.gameObject);
            }
            else
            {
                block.blockHealth = 1;
                block.DestroyBlock();
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[blockHealth - 1];
    }

    private void UpdateScore()
    {
        score += numberOfHits * 100;
    }

    private bool IsNeedToCreatePickUp()
    {
        if (pickUpPrefabs == null || pickUpPrefabs.Length == 0)
        {
            return false;
        }

        int chance = Random.Range(1, 101);
        {
            return chance <= chanceToDrop;
        }
    }

    #endregion
}
