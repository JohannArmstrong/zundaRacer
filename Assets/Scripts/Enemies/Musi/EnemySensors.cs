using UnityEngine;

public class EnemySensors : MonoBehaviour
{
    [SerializeField] private LayerMask floorMask;

    private BoxCollider2D col;
    private Rigidbody2D rb;

    public bool IsGrounded { get; private set; }

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // --- GROUND CHECK ---
    public void CheckGround()
    {
        Vector2 size = col.size * transform.lossyScale;
        Vector2 offset = col.offset * transform.lossyScale;

        float footY = offset.y - size.y * 0.5f;
        Vector2 rayPos = (Vector2)transform.position + new Vector2(0, footY - 0.05f);
        Vector2 raySize = new Vector2(size.x * 0.9f, 0.1f);

        RaycastHit2D hit = Physics2D.BoxCast(
            rayPos,
            raySize,
            0,
            Vector2.zero,
            0,
            floorMask
        );

        IsGrounded = hit.collider != null;
    }

    // --- WALL AHEAD ---
    public bool WallAhead(float direction)
    {
        if (direction == 0) return false;

        Bounds b = col.bounds;

        Vector2 checkPos = new Vector2(
            b.center.x + (b.extents.x + 0.05f) * Mathf.Sign(direction),
            b.center.y
        );

        Vector2 boxSize = new Vector2(
            0.05f,
            b.size.y * 0.8f
        );

        return Physics2D.OverlapBox(
            checkPos,
            boxSize,
            0f,
            floorMask
        );
    }
}
