using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    #region Public methods

    public void ExitGame()
    {
        Application.Quit();
        Debug.LogError("QUIT");
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        Debug.LogError($"Load Scene {index}");
    }

    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Debug.LogError("Load Scene 0");
    }

    #endregion
}
