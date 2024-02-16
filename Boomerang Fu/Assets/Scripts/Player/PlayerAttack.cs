using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BoomerangCollision BoomerangCollision { get; private set; }

    private void AttachBoomerang(BoomerangCollision boomerangBehaviour)
    {
        BoomerangCollision = boomerangBehaviour;
        boomerangBehaviour.GetComponent<Renderer>().materials[0].SetColor("_Color", GameManager.Instance.Color[GetComponent<PlayerMain>().Id]);
    }

    private void Start()
    {
        var input = GetComponentInChildren<PlayerInputHandler>();
        input.OnAttack += Attack;
        BoomerangCollision = GetComponentInChildren<BoomerangCollision>();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (context.canceled && BoomerangCollision != null)
        {
            BoomerangCollision.GetComponent<BoomerangMovement>().StartAction();
            BoomerangCollision = null;
        }
    }
}
