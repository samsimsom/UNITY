using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [HideInInspector] public float lookAngle;
    [HideInInspector] public float pivotAngle;
    public Transform cameraPivot;

    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float minCollisionOffset = 0.2f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;
    [SerializeField] private float cameraFollowSpeed = 0.2f;
    [SerializeField] private float cameraLookSpeed = 0.2f;
    [SerializeField] private float cameraPivotSpeed = 0.2f;
    [SerializeField] private float minPivotAngle = -35f;
    [SerializeField] private float maxPivotAngle = 35f;
    [SerializeField] private float cameraCollisionRadius = 0.2f;

    private InputManager _inputManager;
    private Transform _targetTransform;
    private Transform _cameraTransform;
    private Vector3 _cameraFollowVelocity = Vector3.zero;
    private float _defaultPosition;
    private Vector3 _cameraVectorPosition;

    private RaycastHit _cameraRayHit;

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamere();
    }


    public void HandleCameraCollisionUpdate()
    {
        HandleCameraCollisions();
    }
    
    
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, _targetTransform.position, 
            ref _cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }


    private void RotateCamere()
    {
        Vector3 rotation;
        Quaternion targetRotation;
            
        lookAngle = lookAngle + (_inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (_inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
    

    private void HandleCameraCollisions()
    {
        float targetPosition = _defaultPosition;
        RaycastHit hit;
        Vector3 direction = _cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, 
            cameraCollisionRadius, direction, out hit, 
            Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);
            
            _cameraRayHit = hit;
        }

        if (Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition -= minCollisionOffset;
        }

        _cameraVectorPosition.z = Mathf.Lerp(_cameraTransform.localPosition.z,
            targetPosition, 0.2f);
        _cameraTransform.localPosition = _cameraVectorPosition;
    }
    
    
    #region MonoBehavior
    // -------------------------------------------------------------------------
    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _targetTransform = FindObjectOfType<PlayerManager>().transform;
        _cameraTransform = Camera.main.transform;
        _defaultPosition = _cameraTransform.localPosition.z;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_cameraVectorPosition, 0.5f);
        Gizmos.DrawWireSphere(_cameraRayHit.point, 0.2f);
    }
    
    // -------------------------------------------------------------------------
    #endregion

}
