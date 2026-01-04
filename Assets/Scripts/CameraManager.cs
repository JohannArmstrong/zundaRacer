using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("振動する時間")]
    private float shakeTime;
    [SerializeField, Header("振動の大きさ")]
    private float shakeMagnitude;

    [SerializeField] private float yPermission;
    [SerializeField] private float xPermission;

    private Player player;
    private float shakeCount;
    private int currentPlayerHP;
    private Vector3 _initPos;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        currentPlayerHP = player.GetHP();
        _initPos = transform.position;
    }

    void Update()
    {
        ShakeCheck();
        FollowPlayer();
    }


    private void ShakeCheck()
    {
        if (currentPlayerHP != player.GetHP())
        {
            currentPlayerHP = player.GetHP();
            shakeCount = 0.0f;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 initPos = transform.position;

        while (shakeCount < shakeTime)
        {
            float x = initPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = initPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(x, y, initPos.z);

            shakeCount += Time.deltaTime;

            yield return null;
        }
        transform.position = initPos;
    }

    /*private void FollowPlayer() //original from video
    {
        float x = player.transform.position.x;
        x = Mathf.Clamp(x, _initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }*/

    /*private void FollowPlayer() //invento raro
    {
        float x = player.transform.position.x;
        float y = transform.position.y;

        if ( +y - (+player.transform.position.y) > yPermission )
        {
            y = player.transform.position.y;
            y = Mathf.Clamp(y, _initPos.y, Mathf.Infinity);
        }

        x = Mathf.Clamp(x, _initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, y, transform.position.z);

    }*/

    /*private void FollowPlayer() //robotito1
    {
        Vector2 camPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        float distancia = Vector2.Distance(camPos, playerPos);

        if (distancia > yPermission)
        {
            Vector2 nuevaPos = Vector2.MoveTowards(camPos, playerPos, distancia - yPermission);

            transform.position = new Vector3(nuevaPos.x, nuevaPos.y, transform.position.z);
        }
    }*/

    private void FollowPlayer()
    {
        float camX = transform.position.x;
        float camY = transform.position.y;

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        if (Mathf.Abs(playerX - camX) > xPermission)
        {
            camX = playerX - Mathf.Sign(playerX - camX) * xPermission;
            camX = Mathf.Clamp(camX, _initPos.x, Mathf.Infinity);
        }

        if (Mathf.Abs(playerY - camY) > yPermission)
        {
            camY = playerY - Mathf.Sign(playerY - camY) * yPermission;
            camY = Mathf.Clamp(camY, _initPos.y, Mathf.Infinity);
        }

        transform.position = new Vector3(camX, camY, transform.position.z);
    }

}