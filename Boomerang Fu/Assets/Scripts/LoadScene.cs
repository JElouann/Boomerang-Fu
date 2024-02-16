using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        // Load a new scene
        SceneManager.LoadScene(sceneName);

        // Reset timescale to be sure everything load correctly
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        // Quit game (BUILD)
        Application.Quit();

#if UNITY_EDITOR
        // Quit game (EDITOR/PLAYMODE)
        EditorApplication.ExitPlaymode();
#endif
    }
}
