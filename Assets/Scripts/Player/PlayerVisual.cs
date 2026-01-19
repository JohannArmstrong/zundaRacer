using UnityEngine;
using System.Collections;


public class PlayerVisual : MonoBehaviour
{
    private PlayerHealth health;
    private SpriteRenderer sr;
    private Coroutine blinkRoutine;

    void Awake()
    {
        health = GetComponentInParent<PlayerHealth>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetFacing(float xDir)
    {
        if (Mathf.Abs(xDir) < 0.01f) return;

        if (xDir > 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else if (xDir < 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void StartClientBlink()
    {
        if (sr == null) return;

        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);

        blinkRoutine = StartCoroutine(ClientBlink());
    }

    IEnumerator ClientBlink()
    {
        float t = 0f;
        float duration = health.InvulnerableTime;

        while (t < duration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }

        sr.enabled = true;
        blinkRoutine = null;
    }
}
