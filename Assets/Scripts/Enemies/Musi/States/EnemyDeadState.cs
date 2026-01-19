using Mirror;
using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    private EnemyStateMachine enemy;
    private float destroyDelay = 1.5f;

    public EnemyDeadState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        // detener movimiento
        enemy.Motor.Stop();

        // desactivar colisiones
        Collider2D col = enemy.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // animación
        enemy.Visual.SetDead();

        // destruir en red
        enemy.StartCoroutine(DestroyAfterDelay());
    }

    public void Tick() { }
    public void FixedTick() { }
    public void Exit() { }

    private System.Collections.IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        if (enemy.isServer)
            NetworkServer.Destroy(enemy.gameObject);
    }
}

