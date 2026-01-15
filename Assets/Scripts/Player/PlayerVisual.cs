using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private float lastDir = 1f;

    public void SetFacing(float xDir)
    {
        if (Mathf.Abs(xDir) < 0.01f) return;

        lastDir = Mathf.Sign(xDir);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * lastDir;
        transform.localScale = scale;
    }
}
