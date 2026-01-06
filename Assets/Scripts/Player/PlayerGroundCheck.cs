using Mirror;
using UnityEngine;

public class PlayerGroundCheck : NetworkBehaviour
{
    [SerializeField] LayerMask floorMask;
    [SerializeField] CapsuleCollider2D col;

    [SyncVar] public bool IsGrounded;

    void FixedUpdate()
    {
        if (!isServer) return;

        IsGrounded = Physics2D.OverlapBox(
            col.bounds.center + Vector3.down * col.bounds.extents.y,
            new Vector2(col.bounds.size.x * 0.9f, 0.1f),
            0,
            floorMask
        );
    }
}
