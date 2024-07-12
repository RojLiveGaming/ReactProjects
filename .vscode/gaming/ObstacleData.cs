// ObstacleEditor.cs edstyle
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObstacleData data = (ObstacleData)target;

        for (int x = 0; x < 10; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int z = 0; z < 10; z++)
            {
                data.obstacles[x, z] = EditorGUILayout.Toggle(data.obstacles[x, z]);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(target);
    }
}