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
        currentState?.FixedTick();
    }

    [Server]
    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
