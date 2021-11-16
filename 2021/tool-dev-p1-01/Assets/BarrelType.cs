using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BarrelType : ScriptableObject
{ 
    [Range(1f, 10f)] public float radius = 1;
    public float damage = 10;
    public Color color = Color.black;

    // public MyClass thing;
    // public List<OtherClass> things = new List<OtherClass>();
}

/*
public class MyClass
{
    public Vector3 position;
    public Color color;
}

[Serializable]
public class OtherClass : MyClass
{
    public Vector2 coordinate;
    public float speed;
    [Range(1, 10)] public int range;
}
*/