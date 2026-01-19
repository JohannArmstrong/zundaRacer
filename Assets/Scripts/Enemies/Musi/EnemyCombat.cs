using Mirror;
using UnityEngine;

public class EnemyCombat : NetworkBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int attackPower = 1;

    [SyncVar]
    private int hp;

    private EnemyStateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    public override void OnStartServer()
    {
        hp = maxHp;
    }

    // --- DAÑO AL JUGADOR ---
    [Server]
    public void DealDamage(PlayerHealth player)
    {
        if (player == null) return;
        player.TakeDamage(attackPower);
    }

    // --- RECIBIR DAÑO ---
    [Server]
    public void TakeDamage(int amount)
    {
        if (hp <= 0) return;

        hp -= amount;

        if (hp <= 0)
        {
            Die();
        }
    }

    [Server]
    private void Die()
    {
        stateMachine.ChangeState(new EnemyDeadState(stateMachine));
    }
}
