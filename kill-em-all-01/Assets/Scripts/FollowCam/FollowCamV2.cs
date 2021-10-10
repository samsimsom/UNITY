using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamV2 : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] [Range(0.01f, 1.0f)] private float smoothSpeed = 0.125f;
    
    private Vector3 _velocity = Vector3.zero;


    void Update()
    {
        Vector3 desiredPosition = _target.position;
        
        transform.position = Vector3.SmoothDamp(transform.position,
            desiredPosition, ref _velocity, smoothSpeed);
    }
}