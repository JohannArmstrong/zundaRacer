using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHpChanged))]
    private int hp;

    [SerializeField] private int maxHp = 5;

    public int HP => hp;
    public int MaxHP => maxHp;

    public override void OnStartServer()
    {
        hp = maxHp;
    }

    [Server]
    public void TakeDamage(int amount)
    {
        if (hp <= 0) return;

        hp = Mathf.Max(hp - amount, 0);
    }

    void OnHpChanged(int oldHp, int newHp)
    {
        // Vacío a propósito
        // La UI del cliente va a reaccionar a esto
    }

    [ClientRpc]
    public void RpcBounce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        rb.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);
    }
}
