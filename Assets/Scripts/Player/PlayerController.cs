using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;
    PlayerGroundCheck ground;

    [SyncVar] Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<PlayerGroundCheck>();
    }

    void FixedUpdate()
    {
        if (!isServer) return;

        rb.linearVelocity = velocity;
    }

    [Command]
    public void CmdMove(Vector2 input)
    {
        velocity.x = input.x * moveSpeed;
    }

    [Command]
    public void CmdJump()
    {
        if (!ground.IsGrounded) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
