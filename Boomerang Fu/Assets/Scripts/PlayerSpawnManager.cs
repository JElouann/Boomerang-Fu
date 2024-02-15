using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    private Random _rand;

    private void Awake()
    {
        GetComponent<PlayerInputManager>().onPlayerJoined += OnPlayerJoined;
        _rand = new Random((ulong)Time.realtimeSinceStartup+10);
    }
    void OnPlayerJoined(PlayerInput input)
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Respawn");

        GameObject spawnChoosen = spawnpoints[_rand.NextInt() % 4];

        input.transform.parent.position = spawnChoosen.transform.position;
    }
}
