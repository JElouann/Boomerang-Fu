using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _uiPause; // on récupère le panel d'UI pour la pause

    public void Pause(InputAction.CallbackContext value) //Pour ajouter et retirer la pause grâce à l'input associé
    {
        if(_uiPause.activeSelf == true) // si l'écran est déjà en pause
        {
            // on peut rejouer
            _uiPause.SetActive(false);
            Time.timeScale = 1.0f;
        }

        else // sinon
        {
            // on met l'écran en pause
            Time.timeScale = 0.0f;
            _uiPause.SetActive(true);
        }
    }

    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>(); // on récupère l'input handler
        _input.OnPause += Pause; // ajouter la méthode de pause à l'input de pause
    }

}
