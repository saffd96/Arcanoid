using UnityEngine;

public class LivesView : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject livePrefab;
    [SerializeField] private GameObject canvasGameObject;

    [Header("For DEV Only")]
    [SerializeField] private GameObject[] livesImages;

    private int lives;

    #endregion


    #region Public methods

    public void Setup(int livesCount)
    {
        livesImages = new GameObject[livesCount];

        for (int i = 0; i < livesImages.Length; i++)
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

                Debug.Log("Added 1 live");

                break;
            }
        }
    }

    public void RemoveLife(int currentLifes)
    {
        if (currentLifes != 0)
        {
            Destroy(livesImages[currentLifes]);
        }
    }

    #endregion


    #region Private methods

    private void CreateLife(int i)
    {
        livesImages[i] = Instantiate(livePrefab, new Vector3(10f + 60 * i, 10f), Quaternion.identity);
        livesImages[i].transform.SetParent(canvasGameObject.transform, false);
    }

    #endregion
}
