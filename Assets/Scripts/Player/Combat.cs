using Mirror;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    PlayerHealth health;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Enemy")) return;

        health.TakeDamage(1);
    }
}
