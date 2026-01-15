using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;
    PlayerGroundCheck ground;
    
    private PlayerVisual visual;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<PlayerGroundCheck>();
        visual = GetComponentInChildren<PlayerVisual>();
    }

    
    [Client]
    public void SetMove(Vector2 dir)
    {
        if (!isOwned) return;

        visual?.SetFacing(dir.x);
        CmdMove(dir.x);
    }

    [Client]
    public void SetJump()
    {
        if (!isOwned) return;
        CmdJump();
    }

    // SERVIDOR
    [Command]
    void CmdMove(float x)
    {
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    [Command]
    void CmdJump()
    {
        if (!ground.IsGrounded) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
