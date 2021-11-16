using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class ExplosiveBarrelManager : MonoBehaviour
{
    public static List<ExplosiveBarrel> AllBarrels = new List<ExplosiveBarrel>();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Handles icin display secenekleri.
        Handles.zTest = CompareFunction.LessEqual;

        // List icerisindeki elementlerin herbiri icin
        // calistirilan islemeler.
        foreach (ExplosiveBarrel barrel in AllBarrels)
        {
            Vector3 managerPos = transform.position;
            Vector3 barrelPos = barrel.transform.position;
            float halfHeiht = (managerPos.y - barrelPos.y) * 0.5f;
            Vector3 offset = Vector3.up * halfHeiht;
            
            Handles.DrawBezier(
                managerPos,
                barrelPos,
                (managerPos - offset),
                (barrelPos + offset),
                barrel.type.color,
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