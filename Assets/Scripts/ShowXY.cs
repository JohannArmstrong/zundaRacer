using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SceneMousePosition
{
    static SceneMousePosition()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e.type == EventType.MouseMove)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            Vector3 pos = ray.origin;
            Debug.Log($"Mouse World Position: X={pos.x}, Y={pos.y}");
        }
    }
}
