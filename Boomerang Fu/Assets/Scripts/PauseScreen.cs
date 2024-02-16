using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    // on récupère le panel d'UI pour la pause
    private GameObject _uiPause;

    private void Awake()
    {

        // on cherche le panel d'UI pour la pause
        _uiPause = GameObject.FindGameObjectWithTag("PauseScreen");

        // on récupère l'input handler
        var _input = GetComponentInChildren<PlayerInputHandler>();
        // ajouter la méthode de pause à l'input de pause
        _input.OnPause += Pause;

        foreach (var button in _uiPause.transform.GetChild(0).GetComponentsInChildren<Button>())
        {
            if (button.name == "RetourAuJeu")
            {
                button.onClick.AddListener(UnPause);
            } else {
                button.onClick.AddListener(GoToExit);
            }
        }
    }

    //Pour ajouter et retirer la pause grâce à l'input associé
    public void Pause(InputAction.CallbackContext value) 
    {
        // si l'écran est déjà en pause
        if(_uiPause.transform.GetChild(0).gameObject.activeSelf == true) 
        {
            // on appelle l'autre méthode pour éviter les doublons
            UnPause();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // on met l'écran en pause
            Time.timeScale = 0.0f;
            _uiPause.transform.GetChild(0).gameObject.SetActive(true);

            // Force to select a button to make the controller navigate in the menu
            _uiPause.transform.GetChild(0).GetComponentInChildren<Button>().Select();
        }
    }

    public void UnPause() 
    {
        // on peut rejouer
        _uiPause.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GoToExit()
    {
        SceneManager.LoadScene("Menu");
    }

}
