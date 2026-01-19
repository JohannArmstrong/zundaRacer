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
        if (player == null) return;

        // ¿El player cayó encima?
        if (PlayerCameFromAbove(collision))
        {
            TakeDamage(1);
            player.RpcBounce();
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
    // [ServerCallback]
    // void OnCollisionEnter2D(Collision2D collision)
    // {

    //     Debug.Log("COLLISION on SERVER");
    //     Debug.Log("Collider name: " + collision.collider.name);
    //     Debug.Log("Collider type: " + collision.collider.GetType());
    //     Debug.Log("Collider GameObject: " + collision.collider.gameObject.name);

    //     var playerHealth = collision.collider.GetComponent<PlayerHealth>();
    //     Debug.Log("PlayerHealth on collider: " + (playerHealth != null));

    //     var playerHealthOnGO = collision.collider.gameObject.GetComponent<PlayerHealth>();
    //     Debug.Log("PlayerHealth on GameObject: " + (playerHealthOnGO != null));

    //     // Debug.Log("COLLISION detected on " + (isServer ? "SERVER" : "CLIENT"));

    //     // PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
    //     // if (player == null)
    //     // {
    //     //     Debug.Log("player collision collider null");
    //     //     return;
    //     // }
    //     // Analizamos el contacto
    //     // foreach (ContactPoint2D contact in collision.contacts)
    //     // {
    //     //     // Player viene desde arriba
    //     //     if (contact.normal.y > 0.5f)
    //     //     {
    //     //         OnStompedByPlayer(player);
    //     //         return;
    //     //     }
    //     // }

    //     // // Si no fue stomp → daño al player
    //     // player.TakeDamage(contactDamage);
    // }

    [Server]
    void OnStompedByPlayer(PlayerHealth player)
    {
        TakeDamage(1);

        // rebote del player (RPC visual)
        player.RpcBounce();
    }


    // [ServerCallback]
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     TryDamage(other);
    // }

    // void TryDamage(Collider2D col)
    // {
    //     PlayerHealth player = col.GetComponent<PlayerHealth>();
    //     if (player == null) return;

    //     player.TakeDamage(contactDamage);
    // }

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
