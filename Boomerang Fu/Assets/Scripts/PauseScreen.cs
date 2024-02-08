using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public event Action OnPause;

    [SerializeField] private GameObject _uiPause;

    public void DisplayPauseScreen()
    {
        Time.timeScale = 0.0f;
        _uiPause.SetActive(true);
    }

    public void DisplayGameScreen()
    {
        Time.timeScale = 1.0f;
        _uiPause.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause?.Invoke();
        }

        this.OnPause += DisplayPauseScreen;
    }

}
