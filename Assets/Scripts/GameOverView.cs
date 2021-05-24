using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    #region Variables

    [SerializeField] private Text gameOverText;
    [SerializeField] private GameManager gameManager;

    #endregion


    #region Public methods

    public void SetUnactive()
    {
        gameObject.SetActive(false);
    }

    public void SetTotalScore(int totalScore)
    {
        gameOverText.text = $"You lose\nYour score: {totalScore}";
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    #endregion
}
