using UnityEngine;

public class EfficientVisibilityKiller : MonoBehaviour
{
    private Renderer myRenderer;
    private Plane[] frustumPlanes;
    private bool wasVisible = false;
    private bool hasBeenSeen = false;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Camera.main == null || myRenderer == null)
            return;

        // Solo recalculamos los planos si la cámara se mueve (normal en juegos)
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, myRenderer.bounds);

        if (isVisible)
        {
            hasBeenSeen = true;
        }
        else if (hasBeenSeen)
        {
            Vector3 cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
            if (transform.position.x < cameraMinPos.x)
            {
                // Fue visto y ahora está fuera → destruir
                Destroy(gameObject);
            }
        }

        wasVisible = isVisible;
    }
}
