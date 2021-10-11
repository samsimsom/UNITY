using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] [Range(0.01f, 2.0f)] private float _smoothSpeed = 2.0f;
    
    
    void FixedUpdate()
    {
        Vector3 desirePosition = _target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
            desirePosition, _smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
