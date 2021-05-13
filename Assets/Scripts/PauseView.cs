using UnityEngine;

public class PauseView : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameManager gameManager;

    #endregion


    #region Public Methods

    public void PauseToggle()
    {
        gameManager.IsGamePaused = !gameManager.IsGamePaused;

        Time.timeScale = gameManager.IsGamePaused ? 0f : 1f;

        gameObject.SetActive(gameManager.IsGamePaused);
    }

    public void ContinueGame()
    {
        PauseToggle();
        Debug.LogError("Continue");
    }

    public void SetUnactive()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
