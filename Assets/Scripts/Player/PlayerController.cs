using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb;
    PlayerGroundCheck ground;
    private PlayerVisual visual;

    private bool canSendInput = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<PlayerGroundCheck>();
        visual = GetComponentInChildren<PlayerVisual>();
    }

    // =========================
    // CLIENT → INPUT
    // =========================

    [Client]
    public void SetMove(Vector2 dir)
    {
        if (!isOwned) return;
        if (!canSendInput) return;

        visual?.SetFacing(dir.x);
        CmdMove(dir.x);
    }

    [Client]
    public void SetJump()
    {
        if (!isOwned) return;
        if (!canSendInput) return;

        CmdJump();
    }

    // =========================
    // SERVER → MOVEMENT
    // =========================

    [Command]
    void CmdMove(float x)
    {
        //if (!serverCanMove) return;
        if (!CanServerMove()) return;

        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
    }

    [Command]
    void CmdJump()
    {
        //if (!serverCanMove) return;
        if (!CanServerMove()) return;
        if (!ground.IsGrounded) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // =========================
    // SERVER → MOVEMENT
    // =========================

    [ClientRpc]
    public void RpcBounce()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        rb.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);
    }

    // =========================
    // GAME FLOW EVENTS
    // =========================

    void OnEnable()
    {
        MatchEvents.OnStateChanged += OnMatchStateChanged;
    }

    void OnDisable()
    {
        MatchEvents.OnStateChanged -= OnMatchStateChanged;
    }

    void OnMatchStateChanged(MatchState state)
    {
        switch (state)
        {
            case MatchState.Waiting:
            case MatchState.Countdown:
                canSendInput = true;
                break;

            case MatchState.Playing:
                canSendInput = true;
                break;

            case MatchState.Finished:
                canSendInput = false;
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }

    bool CanServerMove()
    {
        if (GameFlowManager.Instance == null)
            return true; // fallback seguro

        return GameFlowManager.Instance.State != MatchState.Finished;
    }

}
