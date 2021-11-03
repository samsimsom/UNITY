using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExampleOne : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        foreach (var contact in other.contacts)
        {
            PlaceRedSphereAtPoint(contact.point);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Renderer>().material.color = Color.green;
    }

    
    private static void PlaceRedSphereAtPoint(Vector3 point)
    {
        Transform sphereTransform = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        sphereTransform.position = point;
        sphereTransform.localScale = Vector3.one * 0.15f;
        sphereTransform.GetComponent<Renderer>().material.color = Color.red;
        Destroy(sphereTransform.GetComponent<Collider>());
    }


    private void Start()
    {
        
    }


    private void Update()
    {
        
    }
}
