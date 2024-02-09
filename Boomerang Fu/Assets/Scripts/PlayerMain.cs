using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private PauseScreen _pause;

    private void Awake() // on récupère le script de la pause pour  l'intégrer chez tous les joueurs
    {
        _pause = GetComponent<PauseScreen>();
    }
}
