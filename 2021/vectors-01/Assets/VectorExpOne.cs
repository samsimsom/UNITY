using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VectorExpOne : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform centerPoint;

    [SerializeField] private float distance;
    
    private void OnDrawGizmos()
    {
        Vector3 pointAPos = pointA.position;
        Vector3 pointBPos = pointB.position;
        Vector3 pointCPos = pointC.position;

        Vector3 centerPosition = centerPoint.position;
        float circleRadius = 1f;
        
        Gizmos.DrawLine(centerPosition, pointAPos);
        Gizmos.DrawLine(centerPosition, pointBPos);
        Gizmos.DrawLine(centerPosition, pointCPos);
        
        distance = Vector3.Distance(centerPosition, pointAPos);
        // Gizmos.DrawWireSphere(centerPosition, circleRadius);
        Handles.DrawWireDisc(centerPosition, Vector3.forward, circleRadius);
        
    }


    private void OnGUI()
    {
        GUI.skin.label.fontSize = 35;
        GUI.Label(new Rect(0f, 0f, 
            1000, 1000), pointA.position.ToString());
        GUI.Label(new Rect(0f, 50f, 
            1000, 1000), pointA.position.ToString());
    }

    
    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }
}
