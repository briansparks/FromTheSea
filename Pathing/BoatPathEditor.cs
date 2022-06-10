using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoatPath))]
public class BoatPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var boatPath = (BoatPath)target;

        if (GUILayout.Button("Draw boat path"))
        {
            boatPath.DrawBoatPath();
        }

        if (GUILayout.Button("Hide boat path"))
        {
            boatPath.ResetBoatPath();
        }
    }
}
