using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> AllBarrels = new List<ExplosiveBarrel>();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (ExplosiveBarrel barrel in AllBarrels)
        {
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeiht = (managerPos.y - barrelPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeiht;

            Vector3 barrelSize = barrel.GetComponent<Renderer>().bounds.size;
            Vector3 barrelTopPosition = new Vector3(
                barrel.transform.position.x,
                (barrel.transform.position.y + barrelSize.y * 0.5f) + 0.5f,
                barrel.transform.position.z
            );


            Handles.Label(barrelTopPosition, "Barrel");
            Handles.DrawBezier(
                managerPos,
                barrelPos,
                (managerPos - offset),
                (barrelPos + offset),
                Color.red,
                EditorGUIUtility.whiteTexture,
                1f
            );

            Handles.DrawAAPolyLine(transform.position,
                barrel.transform.position);

            /*
            Gizmos.DrawLine(transform.position, barrel.transform.position);
            */
        }
    }
#endif
    
}