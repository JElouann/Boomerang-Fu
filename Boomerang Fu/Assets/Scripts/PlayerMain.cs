using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDash))]
[RequireComponent(typeof(PlayerMouvement))]
[RequireComponent(typeof(PauseScreen))]
public class PlayerMain : MonoBehaviour
{
    private PauseScreen _pause;
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMouvement _mouvement;
    [HideInInspector]
    public int id;

    // on récupère les scripts pour  les intégrer chez tous les joueurs
    private void Awake() 
    {
        _pause = GetComponent<PauseScreen>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
        _mouvement = GetComponent<PlayerMouvement>();
    }

    private void Start()
    {
        // on parcourt la liste des joueurs
        for (int i = 0; i < GameManager.Instance.Connected.Count; i++) 
        {
            // si un emplacement est vide
            if (GameManager.Instance.Connected[i]==false) 
            {
                // le joueur prend cet emplacement et se verra attribuer un id et un score
                id = i;
                GameManager.Instance.Connected[i] = true;
                GameManager.Instance.Score[i] = 0;
            }
        }
    }

    private void OnDestroy()
    {
        // on détruit le score du joueur quand ce dernier est détruit
        GameManager.Instance.Connected[id] = false; 
    }
}
