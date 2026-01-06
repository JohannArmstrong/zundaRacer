using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public override void OnStartAuthority()
    {
        enabled = true;
    }

    void OnDisable()
    {
        enabled = false;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!hasAuthority) return;
        controller.CmdMove(ctx.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!hasAuthority || !ctx.performed) return;
        controller.CmdJump();
    }
}
