using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 2.0f;
    
    
    void Update()
    {
        Vector3 desirePosition = _target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, 
            desirePosition, 
            _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
