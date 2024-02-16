using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        // Reset timescale to be sure everything load correctly
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
