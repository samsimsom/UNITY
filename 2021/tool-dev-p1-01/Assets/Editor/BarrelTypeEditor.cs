using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BarrelType))]
public class BarrelTypeEditor : Editor
{
    
    public enum MyEnum
    {
        Bleep, Bloop, Blap
    }

    private MyEnum _myEnum;
    
    public override void OnInspectorGUI()
    {
        // explicit positioning using Rect
        // GUI
        // EditorGUI
        
        // implicit positioning, auto-layout
        // GUILayout
        // EditorGUILayout
        
        base.OnInspectorGUI();
        
        DrawUILine(Color.black);
        
        GUILayout.Label("New UI", EditorStyles.boldLabel);
        if (GUILayout.Button("Do Something!"))
        {
            Debug.Log($"Do IT! {_myEnum}");
        }
        
        _myEnum = (MyEnum)EditorGUILayout.EnumPopup(_myEnum);

    }
    
    
    public static void DrawUILine(Color color, 
        int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(
            GUILayout.Height(padding+thickness));
        r.height = thickness;
        r.y+=padding/2;
        r.x-=2;
        r.width +=6;
        EditorGUI.DrawRect(r, color);
    }
    

}

