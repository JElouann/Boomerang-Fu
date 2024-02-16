using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    // Get a new Random object (see Random.cs)
    private Random _rand;

    private void Awake()
    {
        // Get Input manager component to bind the onPlayerJoined event
        GetComponent<PlayerInputManager>().onPlayerJoined += OnPlayerJoined;

        // Initialize the random object
        _rand = new Random((ulong)Time.realtimeSinceStartup + 10);
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        // Get all spawnpoints
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Respawn");

        // Take a random spawnpoint
        GameObject spawnChoosen = spawnpoints[(int)(_rand.NextInt64() % 4)];

        // Set the new player position to the spawnpoint
        input.transform.parent.position = spawnChoosen.transform.position;
    }
}
