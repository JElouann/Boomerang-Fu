using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerDash))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PauseScreen))]
public class PlayerMain : MonoBehaviour
{
    // Store the player ID
    [HideInInspector]
    public int Id;

    // Get all player components
    private PauseScreen _pause;
    private PlayerAttack _attack;
    private PlayerDash _dash;
    private PlayerMovement _mouvement;

    // Get the audio
    private AudioSource _source;

   
    private void Awake()
    {
        // Get every component needed for execution, of every part of the player
        _pause = GetComponent<PauseScreen>();
        _attack = GetComponent<PlayerAttack>();
        _dash = GetComponent<PlayerDash>();
        _mouvement = GetComponent<PlayerMovement>();

        // Get the audio source with the finish tag (arbitrary unused default tag)
        _source = GameObject.FindGameObjectWithTag("Finish").GetComponent<AudioSource>();
    }

    private void Start()
    {
        // We iterate over every player's possible id
        for (int i = 0; i < GameManager.Instance.Connected.Count; i++)
        {
            // If nobody has this id
            if (GameManager.Instance.Connected[i] == false)
            {
                // Acquire this id and store it as this player's
                Id = i;
                GameManager.Instance.Connected[i] = true;

                // Reset its score
                GameManager.Instance.Score[i] = 0;
                break;
            }
        }

        // Get all meshes renderer of that player (there is, like, about 140, my meshes my be unoptimized and poorly done)
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            // Change its color to the player's id color
            renderer.materials[0].SetColor("_Color", GameManager.Instance.Color[Id]);
        }
    }

    private void OnDestroy()
    {
        // Camera shake on death
        Camera.main.DOShakePosition(0.4f, 1);

        // We free the id when we die
        GameManager.Instance.Connected[Id] = false;

        // And player the death audio
        _source.Play();
    }
}
