using Mirror;
using UnityEngine;

public class EnemyCombat : NetworkBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int contactDamage = 1;

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
    [ServerCallback]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
        PlayerController playerController = collision.collider.GetComponent<PlayerController>();

        if (player == null || playerController == null) return;

        // ¿El player cayó encima?
        if (PlayerCameFromAbove(collision))
        {
            TakeDamage(1);
            playerController.RpcBounce();
        }
        else
        {
            player.TakeDamage(contactDamage);
        }
    }

    bool PlayerCameFromAbove(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            // Normal apunta desde el enemigo hacia el player
            if (contact.normal.y < -0.5f)
                return true;
        }
        return false;
    }
    

    // --- RECIBIR DAÑO ---
    [ServerCallback]
    public void TakeDamage(int amount)
    {
        if (hp <= 0) return;

        hp -= amount;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        stateMachine.ChangeState(new EnemyDeadState(stateMachine));
    }
}
