using Mirror;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private bool spawned = false;

    private float halfWidth;

    public override void OnStartServer()
    {
        spawned = false;
        halfWidth = enemyPrefab.transform.lossyScale.x / 2;
    }

    [ServerCallback]
    void Update()
    {
        if (spawned) return;

        foreach (var conn in NetworkServer.connections.Values)
        {
            if (conn.identity == null) continue;

            if (IsNearCameraX(transform.position, halfWidth))
            {
                SpawnEnemy();
            }
        }
    }

    [Server]
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(enemy);
        spawned = true;
    }

    [Server]
    bool IsNearCameraX(Vector3 pos, float margin)
    {
        float x = Camera.main.WorldToViewportPoint(pos).x;
        return x >= -margin && x <= 1f + margin;
    }
}
