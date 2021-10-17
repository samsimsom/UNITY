using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] [Range(0.01f, 2.0f)] private float smoothSpeed = 2.0f;


    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desirePosition = target.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
                desirePosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }
}
