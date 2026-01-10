using Mirror;
using UnityEngine;

public class PlayerGroundCheck : NetworkBehaviour
{
    [SerializeField] LayerMask floorMask;
    
    private CapsuleCollider2D col;

    public bool IsGrounded;

    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();

        if (col == null)
        {
            Debug.LogError("PlayerGroundCheck's CapsuleCollider2D not found in object");
        }
    }

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
