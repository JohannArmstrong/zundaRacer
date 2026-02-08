using Mirror;
using UnityEngine;

public class EnemyStateMachine : NetworkBehaviour
{
    private IEnemyState currentState;

    public EnemyMotor Motor { get; private set; }
    public EnemySensors Sensors { get; private set; }
    public EnemyVisual Visual { get; private set; }

    void Awake()
    {
        Motor = GetComponent<EnemyMotor>();
        Sensors = GetComponent<EnemySensors>();
        Visual = GetComponentInChildren<EnemyVisual>();
    }

    public override void OnStartServer()
    {
        ChangeState(new EnemyPatrolState(this));
    }

    // DECISIÓN / IA
    [ServerCallback]
    void Update()
    {
        currentState?.Tick();
    }

    // FÍSICA / MOVIMIENTO
    [ServerCallback]
    void FixedUpdate()
    {
        Sensors.CheckGround();
        currentState?.FixedTick();
    }

    [Server]
    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    void OnEnable()
    {
        MatchEvents.OnStateChanged += OnMatchStateChanged;
    }

    void OnDisable()
    {
        MatchEvents.OnStateChanged -= OnMatchStateChanged;
    }

    [Server]
    void OnMatchStateChanged(MatchState state)
    {
        if (state == MatchState.Finished)
        {
            Freeze();
        }
    }

    [Server]
    void Freeze()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        Visual.SetIdle(true);
        enabled = false; // detiene la FSM
    }

}
