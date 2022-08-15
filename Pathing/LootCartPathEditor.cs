using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LootCartPath))]
public class LootCartPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var lootCartPath = (LootCartPath)target;

        if (GUILayout.Button("Draw path"))
        {
            lootCartPath.DrawPath();
        }

        if (GUILayout.Button("Hide path"))
        {
            lootCartPath.ResetPath();
        }
    }
}
