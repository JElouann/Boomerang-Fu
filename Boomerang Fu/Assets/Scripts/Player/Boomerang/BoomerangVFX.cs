using UnityEngine;

public class BoomerangVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject _trail;
    [SerializeField]
    private GameObject _deathVFXPrefab;

    public void Enable()
    {
        // Activate trail, and rotate it forward
        _trail.SetActive(true);
        _trail.transform.rotation = gameObject.transform.rotation;
    }

    public void Turn()
    {
        // Turn the trail in the opposite direction
        _trail.transform.localRotation = new Quaternion(0, 180, 0, 0);
    }

    private void Awake()
    {
        // Add the die event 
        GetComponent<BoomerangCollision>().OnDie += Die;
    }

    private void Die(GameObject player)
    {
        // Instantiate the VFX
        GameObject death = Instantiate(_deathVFXPrefab, player.transform);

        // and force it outside any parent, to be sure it's not inside the destroyed player
        death.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Disable the trail if there is a collision
        _trail.SetActive(false);
    }
}
