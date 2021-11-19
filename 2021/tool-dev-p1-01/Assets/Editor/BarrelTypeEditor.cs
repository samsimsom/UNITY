using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// NOTES
// explicit positioning using Rect
// GUI
// EditorGUI
        
// implicit positioning, auto-layout
// GUILayout
// EditorGUILayout

[CustomEditor(typeof(BarrelType))]
public class BarrelTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

