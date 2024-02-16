using UnityEngine;

public class BoomerangVFX : MonoBehaviour
{
    [SerializeField] private GameObject _trail;
    [SerializeField] private GameObject _deathVFXPrefab;

    private void Awake()
    {
        GetComponent<BoomerangCollision>().OnDie += Die;
    }

    private void Die(GameObject player)
    {
        GameObject death = Instantiate(_deathVFXPrefab, player.transform);
        death.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        _trail.SetActive(false);
    }

    public void Enable()
    {
        _trail.SetActive(true);
        _trail.transform.rotation = gameObject.transform.rotation;
    }

    public void Turn()
    {
        _trail.transform.localRotation = new Quaternion(0, 180, 0, 0);
    }
}
