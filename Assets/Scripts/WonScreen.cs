using UnityEngine;
using UnityEngine.UI;

public class WonScreen : MonoBehaviour
{
    #region Variables

    [SerializeField] private Text winText;
    [SerializeField] private GameManager gameManager;

    #endregion


    #region Public methods

    public void SetUnactive()
    {
        gameObject.SetActive(false);
    }

    public void SetTotalScore(int totalScore)
    {
        winText.text = $"Congrats\nYour score: {totalScore}";
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    #endregion
}
