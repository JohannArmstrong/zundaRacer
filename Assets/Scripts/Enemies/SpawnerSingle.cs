using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerSingle : MonoBehaviour
{
    [SerializeField, Header("敵オブジェクト")]
    private GameObject enemy;

    private Player player;
    private GameObject enemyObj;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        enemyObj = null;
    }

    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (!player) return;

        Vector3 playerPos = player.transform.position;
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 scale = enemy.transform.lossyScale;

        float distance = Vector2.Distance(transform.position, new Vector2(player.transform.position.x, transform.position.y));
        float spawnDis = Vector2.Distance(playerPos, new Vector2(cameraMaxPos.x + scale.x / 2.0f, playerPos.y));

        if (distance <= spawnDis && !enemyObj)
        {
            enemyObj = Instantiate(enemy);
            enemyObj.transform.position = transform.position;
            transform.parent = enemyObj.transform;
        }
    }
}