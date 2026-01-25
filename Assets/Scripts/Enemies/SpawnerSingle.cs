using Mirror;
using UnityEngine;

public class SpawnerSingle : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnOffsetX = 12f;

    private bool spawned = false;

    public override void OnStartServer()
    {
        spawned = false;
    }

    [ServerCallback]
    void Update()
    {
        if (spawned) return;

        foreach (var conn in NetworkServer.connections.Values)
        {
            if (conn.identity == null) continue;

            Transform player = conn.identity.transform;

            float dx = Mathf.Abs(player.position.x - transform.position.x);

            if (dx <= spawnOffsetX)
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
}
