using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private float lastDir = 1f;

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
}
