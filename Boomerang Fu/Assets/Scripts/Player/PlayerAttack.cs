using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Attached boomerang
    public BoomerangCollision BoomerangCollision { get; private set; }

    private void AttachBoomerang(BoomerangCollision boomerangBehaviour)
    {
        // Attach the boomerang, keep it in memory and change its color to the player's color
        BoomerangCollision = boomerangBehaviour;
        boomerangBehaviour.GetComponent<Renderer>().materials[0].SetColor("_Color", GameManager.Instance.Color[GetComponent<PlayerMain>().Id]);
    }

    private void Start()
    {
        // Get Input handler and link the Attack input to the Attack method.
        var input = GetComponentInChildren<PlayerInputHandler>();
        input.OnAttack += Attack;

        // Link the currently attached boomerang.
        BoomerangCollision = GetComponentInChildren<BoomerangCollision>();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        // If the input is released and we have a boomerang
        if (context.canceled && BoomerangCollision != null)
        {
            // Start the boomerang action
            BoomerangCollision.GetComponent<BoomerangMovement>().StartAction();

            // Detach the boomerang
            BoomerangCollision = null;
        }
    }
}
