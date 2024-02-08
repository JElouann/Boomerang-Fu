using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _uiPause; // on r�cup�re le panel d'UI pour la pause

    public void Pause(InputAction.CallbackContext value) //Pour ajouter et retirer la pause gr�ce � l'input associ�
    {
        if(_uiPause.activeSelf == true) // si l'�cran est d�j� en pause
        {
            // on peut rejouer
            _uiPause.SetActive(false);
            Time.timeScale = 1.0f;
        }

        else // sinon
        {
            // on met l'�cran en pause
            Time.timeScale = 0.0f;
            _uiPause.SetActive(true);
        }
    }

    private void Start()
    {
        var _input = GetComponentInChildren<PlayerInputHandler>(); // on r�cup�re l'input handler
        _input.OnPause += Pause; // ajouter la m�thode de pause � l'input de pause
    }

}
