using System;
using UnityEngine;

public class BoomerangCollision : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerMain _ownerMain;

    // Variables that stores local transform for when we reattach to the player
    private Vector3 _localPosition;
    private Quaternion _localRotation;

    // Event that is caller when we die, used in BoomerangVFX to create the blood Visual
    public event Action<GameObject> OnDie;

    private void Awake()
    {
        // Grab the owner and keep the PlayerMain component
        var owner = gameObject.transform.parent.gameObject;
        _ownerMain = owner.GetComponent<PlayerMain>();

        _rb = GetComponent<Rigidbody>();

        // Store local transform
        _localPosition = transform.localPosition;
        _localRotation = transform.localRotation;

        // Disable collider and gravity, because you are in a player and don't need to collide with anything
        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        // Check if the latest owner player exist, if yes, destroy itself
        if (_ownerMain == null)
        {
            Destroy(gameObject);
        }
    }

    private void Attach(GameObject parent)
    {
        // Restore transform
        transform.parent = parent.transform;
        transform.localPosition = _localPosition;
        transform.localRotation = _localRotation;

        // Dsable collider and gravity
        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;

        // Tell the parent (Player) to attach the boomerang to itself
        // The message is taken by the PlayerAttack class
        parent.SendMessage("AttachBoomerang", this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // If we hit a player
        if (collision.gameObject.tag == "Player")
        {
            // And that player is our owner
            if (collision.gameObject == _ownerMain.gameObject)
            {
                // And our owner hasn't grabbed a new Boomerang
                if (_ownerMain.gameObject.GetComponent<PlayerAttack>().BoomerangCollision == null)
                {
                    // Reattach
                    Attach(collision.gameObject);
                }
            }

            // And that player isn't our owner
            else
            {
                // And the boomerang is stopped (or almost stopped)
                // And the Player it collided with have no boomerang in their hand
                if (_rb.velocity.magnitude < 0.3 && collision.gameObject.GetComponent<PlayerAttack>().BoomerangCollision == null)
                {
                    // Reattach and update the owner
                    var owner = collision.gameObject;
                    _ownerMain = owner.GetComponent<PlayerMain>();
                    Attach(collision.gameObject);
                }

                // And the boomerang is blazing fast, or the player it collided with has a boomerang in their hand
                else
                {
                    // Invoke the death event
                    OnDie?.Invoke(collision.gameObject);

                    // Increment the throwing player score
                    GameManager.Instance.Score[_ownerMain.Id]++;

                    // And kill the other player
                    Destroy(collision.gameObject);
                }
            }
        }

        // Change color to a darker one, to let the player notice the boomerang is on the ground
        GetComponent<Renderer>().materials[0].SetColor("_Color", GetComponent<Renderer>().materials[0].color * 0.75f);
    }
}
