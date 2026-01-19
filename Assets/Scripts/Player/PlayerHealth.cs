using Mirror;
using UnityEngine;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHpChanged))]
    private int hp;

    [SerializeField] private int maxHp = 5;
    [SerializeField] public float invulnerableTime { get; private set; } = 1.0f;

    // Tiempo hasta el cual el jugador es invulnerable (server)
    private double invulnerableUntil;


    private PlayerVisual visual;

    public int HP => hp;
    public int MaxHP => maxHp;
    public float InvulnerableTime => invulnerableTime;

    void Awake()
    {
        visual = GetComponentInChildren<PlayerVisual>();
    }

    public override void OnStartServer()
    {
        hp = maxHp;
        invulnerableUntil = 0;
    }



    [Server]
    public void TakeDamage(int amount)
    {
        // invulnerabilidad por tiempo (determinista)
        if (NetworkTime.time < invulnerableUntil) return;
        if (hp <= 0) return;

        hp = Mathf.Max(hp - amount, 0);
        invulnerableUntil = NetworkTime.time + invulnerableTime;

        RpcOnDamaged();

        if (hp <= 0)
        {
            //Die();
        }
    }


    void OnHpChanged(int oldHp, int newHp)
    {
        // Vacío a propósito
        // La UI del cliente va a reaccionar a esto
    }

    [ClientRpc]
    void RpcOnDamaged()
    {
        visual?.StartClientBlink();
    }
    
    
}
