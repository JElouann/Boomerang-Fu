using UnityEngine;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

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

    private AudioSource _source;

    [HideInInspector]
    public int id;
    //[SerializeField] private GameObject _spawnVFX;
    private Camera _mainCamera;

    // on r�cup�re les scripts pour  les int�grer chez tous les joueurs
    private void Awake() 
    {
        _pause = GetComponent<PauseScreen>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
        _mouvement = GetComponent<PlayerMouvement>();
        _source = GameObject.FindGameObjectWithTag("Finish").GetComponent<AudioSource>();
        _mainCamera = Camera.main;
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
                break;
            }
            //GameObject spawnVFX = Instantiate(_spawnVFX);
            //spawnVFX.transform.parent = null;
        }

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.materials[0].SetColor("_Color", GameManager.Instance.Color[id]);
        }
    }

    private void OnDestroy()
    {
        _mainCamera.DOShakePosition(0.4f, 1);
        // on d�truit le score du joueur quand ce dernier est d�truit
        GameManager.Instance.Connected[id] = false; 
        _source.Play();
     
    }
}
