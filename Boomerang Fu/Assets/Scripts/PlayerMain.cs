using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private PauseScreen _pause;

    private void Awake() // on r�cup�re le script de la pause pour  l'int�grer chez tous les joueurs
    {
        _pause = GetComponent<PauseScreen>();
    }
}
