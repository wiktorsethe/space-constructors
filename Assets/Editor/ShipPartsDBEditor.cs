using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShipPartsDatabase))]
public class ShipPartsDBEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShipPartsDatabase shipPartsDB = (ShipPartsDatabase)target;
        EditorGUILayout.Space();
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 16;
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("RESET TO DEFAULT", buttonStyle, GUILayout.Height(40), GUILayout.Width(200)))
        {
            shipPartsDB.Reset();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("shipParts"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
