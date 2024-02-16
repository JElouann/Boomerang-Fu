using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    // on récupère le panel d'UI pour la pause
    private GameObject _uiPause;

    // Pour ajouter et retirer la pause grâce à l'input associé
    public void Pause(InputAction.CallbackContext value)
    {
        // si l'écran est déjà en pause
        if (_uiPause.transform.GetChild(0).gameObject.activeSelf == true)
        {
            // We unpause, unpause has been made into a function 'cause we need it a lot
            UnPause();
        }
        else
        {
            // We unlock the cursor, we are in UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // We freeze time to pause, and show the panel
            Time.timeScale = 0.0f;
            _uiPause.transform.GetChild(0).gameObject.SetActive(true);

            // Force to select a button to make the controller navigate in the menu
            _uiPause.transform.GetChild(0).GetComponentInChildren<Button>().Select();
        }
    }

    public void UnPause()
    {
        // We turn the game back on and hide the panel
        _uiPause.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1.0f;

        // We relock the cursor, we are in gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GoToExit()
    {
        // Simply load Main Menu
        SceneManager.LoadScene("Menu");
    }

    private void Awake()
    {
        // We search the pause panel UI
        _uiPause = GameObject.FindGameObjectWithTag("PauseScreen");

        // We get the input handler of the attached player
        var input = GetComponentInChildren<PlayerInputHandler>();

        // We add the pause event
        input.OnPause += Pause;

        // For each button, we add the listener
        foreach (var button in _uiPause.transform.GetChild(0).GetComponentsInChildren<Button>())
        {
            // UnPause button
            if (button.name == "RetourAuJeu")
            {
                button.onClick.AddListener(UnPause);
            }

            // Quit button
            else
            {
                button.onClick.AddListener(GoToExit);
            }
        }
    }
}
