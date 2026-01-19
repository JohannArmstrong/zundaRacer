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
        //Debug.Log("CheckGround ejecutándose", this);


        Bounds b = col.bounds;

        Vector2 rayPos = new Vector2(
            b.center.x,
            b.min.y - 0.05f
        );

        Vector2 raySize = new Vector2(
            b.size.x * 1f,
            0.1f
        );

        Debug.DrawLine(
            rayPos - Vector2.right * raySize.x * 0.5f,
            rayPos + Vector2.right * raySize.x * 0.5f,
            Color.green
        );

        IsGrounded = Physics2D.BoxCast(
            rayPos,
            raySize,
            0f,
            Vector2.zero,
            0f,
            LayerMask.GetMask("Floor")
        );
    }


    // --- WALL AHEAD ---
    public bool WallAhead(float direction)
    {
        //Debug.Log("WallAhead ejecutándose", this);

        if (direction == 0) return false;

        Bounds b = col.bounds;

        Vector2 checkPos = new Vector2(
            direction > 0 ? b.max.x + 0.05f : b.min.x - 0.05f,
            b.center.y
        );

        Vector2 boxSize = new Vector2(
            0.1f,
            b.size.y * 0.8f
        );

        Debug.DrawLine(
            checkPos + Vector2.up * boxSize.y * 0.5f,
            checkPos - Vector2.up * boxSize.y * 0.5f,
            Color.red
        );

        return Physics2D.OverlapBox(
            checkPos,
            boxSize,
            0f,
            LayerMask.GetMask("Floor")
        );
    }

}
