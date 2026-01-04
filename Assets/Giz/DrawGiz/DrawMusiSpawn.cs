using UnityEngine;

public class DrawMusiSpawn : MonoBehaviour
{
    const string imagePath = "Assets/Giz/musi.png";

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, imagePath, true);
    }
}