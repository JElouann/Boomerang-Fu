using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    private Random _rand;

    private void Awake()
    {
        GetComponent<PlayerInputManager>().onPlayerJoined += OnPlayerJoined;
        _rand = new Random((ulong)Time.realtimeSinceStartup + 10);
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Respawn");

        int randNum = (int)(_rand.NextInt64() % 4);
        print(randNum);
        GameObject spawnChoosen = spawnpoints[randNum];

        input.transform.parent.position = spawnChoosen.transform.position;
    }
}
