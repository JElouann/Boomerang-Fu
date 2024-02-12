using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    // on récupère le panel d'UI pour la pause
    [SerializeField] private GameObject _uiPause;
    
    //Pour ajouter et retirer la pause grâce à l'input associé
    public void Pause(InputAction.CallbackContext value) 
    {
        // si l'écran est déjà en pause
        if(_uiPause.activeSelf == true) 
        {
            // on appelle l'autre méthode pour éviter les doublons
            UnPause(); 
        }

        else
        {
            // on met l'écran en pause
            Time.timeScale = 0.0f;
            _uiPause.SetActive(true);
        }
    }

    public void UnPause() 
    {
        // on peut rejouer
        _uiPause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        // on récupère l'input handler
        var _input = GetComponentInChildren<PlayerInputHandler>();
        // ajouter la méthode de pause à l'input de pause
        _input.OnPause += Pause;
    }

}
