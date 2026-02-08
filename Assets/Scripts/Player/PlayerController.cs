using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private float startLineXL = -8f;
    [SerializeField] private float startLineXR = 8f;

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

        // BLOQUEO DURANTE COUNTDOWN
        if (GameFlowManager.Instance.State == MatchState.Countdown)
        {
            Vector2 pos = rb.position;
            float media = (startLineXL - startLineXR) / 2;
            if (pos.x <= media)
                pos.x = Mathf.Max(pos.x, startLineXL);
            else
                pos.x = Mathf.Min(pos.x, startLineXR);

            rb.position = pos;
        }
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
                if (isServer)
                {
                    FreezePhysics();
                }
                break;
        }
    }

    [Server]
    void FreezePhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
    }


    bool CanServerMove()
    {
        if (GameFlowManager.Instance == null)
            return true; // fallback seguro

        return GameFlowManager.Instance.State != MatchState.Finished;
    }

}
