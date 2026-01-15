using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    private PlayerController controller;
    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public override void OnStartAuthority()
    {
        enabled = true;
    }

    void Update()
    {
        if (!isOwned) return;

        controller.SetMove(moveInput);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!isOwned) return;

        // guardamos el input, no ejecutamos lógica
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!isOwned || !ctx.performed) return;
        controller.SetJump();
    }
}

