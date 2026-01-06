using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHpChanged))]
    int hp = 3;

    public bool IsDead => hp <= 0;

    [Server]
    public void TakeDamage(int dmg)
    {
        if (IsDead) return;
        hp = Mathf.Max(hp - dmg, 0);
    }

    void OnHpChanged(int oldHp, int newHp)
    {
        if (newHp <= 0)
        {
            // animación, UI, etc
        }
    }
}
