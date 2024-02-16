using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDash))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PauseScreen))]
public class PlayerMain : MonoBehaviour
{
    [HideInInspector]
    public int Id;

    private PauseScreen _pause;
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _mouvement;

    private AudioSource _source;

    private Camera _mainCamera;

    // on r�cup�re les scripts pour  les int�grer chez tous les joueurs
    private void Awake()
    {
        _pause = GetComponent<PauseScreen>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
        _mouvement = GetComponent<PlayerMovement>();

        _source = GameObject.FindGameObjectWithTag("Finish").GetComponent<AudioSource>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        // on parcourt la liste des joueurs
        for (int i = 0; i < GameManager.Instance.Connected.Count; i++)
        {
            // si un emplacement est vide
            if (GameManager.Instance.Connected[i] == false)
            {
                // le joueur prend cet emplacement et se verra attribuer un id et un score
                Id = i;
                GameManager.Instance.Connected[i] = true;
                GameManager.Instance.Score[i] = 0;
                break;
            }
        }

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.materials[0].SetColor("_Color", GameManager.Instance.Color[Id]);
        }
    }

    private void OnDestroy()
    {
        _mainCamera.DOShakePosition(0.4f, 1);

        // on d�truit le score du joueur quand ce dernier est d�truit
        GameManager.Instance.Connected[Id] = false;
        _source.Play();
    }
}
