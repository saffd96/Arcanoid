using UnityEngine;

public class LivesView : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject livePrefab;
    [SerializeField] private GameObject canvasGameObject;
    [SerializeField] private GameManager gameManager;

    [Header("For DEV Only")]
    [SerializeField] private GameObject[] livesImages;

    private int lives;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        Ball.OnBottomWallCollided += BallLost;
    }

    private void OnDisable()
    {
        Ball.OnBottomWallCollided -= BallLost;
    }
    private void Start()
    {
        //livesImages = new List<GameObject>(gameManager.MaxLives);  из этого ничего не работает почему-то(
        //livesImages = new GameObject[gameManager.CurrentLives-1];  из этого ничего не работает почему-то(
        livesImages = new GameObject[3]; // пришлось создавать вручную(
    }

    #endregion


    #region Public methods

    public void CreateLivesImages()
    {
        for (int i = 0; i < gameManager.MaxLives; i++)
        {
            CreateLife(i);
        }
    }

    public void AddLive()
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            if (livesImages[i] == null)
            {
                CreateLife(i);

                gameManager.CurrentLives++;

                break;
            }
        }
    }
    
    #endregion


    #region Private methods

    private void BallLost()
    {
        if (gameManager.CurrentLives != 0)
        {
            Destroy(livesImages[gameManager.CurrentLives - 1]);
        }
    }

    private void CreateLife(int i)
    {
        livesImages[i] = Instantiate(livePrefab, new Vector3(10f + 60 * i, 10f), Quaternion.identity);
        livesImages[i].transform.SetParent(canvasGameObject.transform, false);
    }

    #endregion
}
