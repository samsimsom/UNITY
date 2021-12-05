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

[CanEditMultipleObjects]
[CustomEditor(typeof(BarrelType))]
public class BarrelTypeEditor : Editor
{
    private SerializedObject so;
    private SerializedProperty propRadius;
    private SerializedProperty propDamege;
    private SerializedProperty propColor;


    private void OnEnable()
    {
        so = serializedObject;
        propRadius = so.FindProperty("radius");
        propDamege = so.FindProperty("damage");
        propColor = so.FindProperty("color");
    }

    
    public override void OnInspectorGUI()
    {
        so.Update();

        EditorGUILayout.PropertyField(propRadius);
        EditorGUILayout.PropertyField(propDamege);
        EditorGUILayout.PropertyField(propColor);

        so.ApplyModifiedProperties();
        
        // BarrelType barrel = target as BarrelType;
        // barrel.radius = EditorGUILayout.FloatField("radius", barrel.radius);
        // barrel.damage = EditorGUILayout.FloatField("damage", barrel.damage);
        // barrel.color = EditorGUILayout.ColorField("color", barrel.color);
    }
}

