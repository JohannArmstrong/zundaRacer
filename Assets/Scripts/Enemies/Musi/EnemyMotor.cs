using UnityEngine;
using Mirror;

public class EnemyMotor : NetworkBehaviour
{
    [SerializeField] float moveSpeed;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    [Server]
    public void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocityY);
        Flip(dir);
    }

    [Server]
    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void Flip(float dir)
    {
        if (dir == 0) return;

        transform.eulerAngles = dir < 0
            ? Vector3.zero
            : new Vector3(0, 180, 0);
    }
}
