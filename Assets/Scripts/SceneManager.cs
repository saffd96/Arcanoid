using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Public methods

    public void ExitGame()
    {
        Application.Quit();
        Debug.LogError("QUIT");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Debug.LogError("Load Scene 1");
    }

    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Debug.LogError("Load Scene 0");
    }

    #endregion
}
