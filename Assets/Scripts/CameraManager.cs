using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float yPermission;
    [SerializeField] private float xPermission;

    private Transform target;
    private PlayerHealth playerHealth;

    private int currentHP;
    private Vector3 initPos;
    private Coroutine shakeRoutine;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (target == null || playerHealth == null) return;

        CheckShake();
        FollowTarget();
    }

    // 🔹 llamado por PlayerNetwork (solo jugador local)
    public void RegisterLocalPlayer(PlayerHealth health)
    {
        playerHealth = health;
        target = health.transform;
        currentHP = health.HP;
    }

    private void CheckShake()
    {
        int hp = playerHealth.HP;
        if (hp == currentHP) return;

        currentHP = hp;

        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(Shake());
    }

    private void FollowTarget()
    {
        Vector3 pos = transform.position;
        Vector3 t = target.position;

        if (Mathf.Abs(t.x - pos.x) > xPermission)
            pos.x = t.x - Mathf.Sign(t.x - pos.x) * xPermission;

        if (Mathf.Abs(t.y - pos.y) > yPermission)
            pos.y = t.y - Mathf.Sign(t.y - pos.y) * yPermission;

        transform.position = new Vector3(pos.x, pos.y, initPos.z);
    }

    private IEnumerator Shake()
    {
        Vector3 basePos = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeTime)
        {
            transform.position = basePos +
                (Vector3)Random.insideUnitCircle * shakeMagnitude;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = basePos;
        shakeRoutine = null;
    }
}
