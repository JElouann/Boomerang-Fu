using System;
using UnityEngine;

public class BoomerangCollision : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerMain _ownerMain;

    private Vector3 _localPosition;
    private Quaternion _localRotation;

    public event Action<GameObject> OnDie;

    private void Awake()
    {
        var owner = gameObject.transform.parent.gameObject;
        _ownerMain = owner.GetComponent<PlayerMain>();

        _rb = GetComponent<Rigidbody>();

        _localPosition = transform.localPosition;
        _localRotation = transform.localRotation;

        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (_ownerMain == null)
        {
            Destroy(gameObject);
        }
    }

    private void Attach(GameObject parent)
    {
        transform.parent = parent.transform;
        transform.localPosition = _localPosition;
        transform.localRotation = _localRotation;

        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;

        parent.SendMessage("AttachBoomerang", this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject == _ownerMain.gameObject)
            {
                if (_ownerMain.gameObject.GetComponent<PlayerAttack>().BoomerangCollision == null)
                {
                    Attach(collision.gameObject);
                }
            }
            else
            {
                if (_rb.velocity.magnitude < 0.3 && collision.gameObject.GetComponent<PlayerAttack>().BoomerangCollision == null)
                {
                    var owner = collision.gameObject;
                    _ownerMain = owner.GetComponent<PlayerMain>();
                    Attach(collision.gameObject);
                }
                else
                {
                    OnDie?.Invoke(collision.gameObject);
                    GameManager.Instance.Score[_ownerMain.Id]++;
                    Destroy(collision.gameObject);
                }
            }
        }

        GetComponent<Renderer>().materials[0].SetColor("_Color", GetComponent<Renderer>().materials[0].color * 0.75f);
    }
}
