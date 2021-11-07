using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLocomotion : MonoBehaviour
{
    #region Class Variables
    // -------------------------------------------------------------------------
    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15f;

    [HideInInspector] public bool isSprinting;
    
    private InputManager _inputManager;
    private Transform _cameraObject;
    private Rigidbody _playerRigidbody;
    private Vector3 _moveDirection;
    
    // -------------------------------------------------------------------------
    #endregion

    
    #region Public Methods
    // -------------------------------------------------------------------------
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    
    // -------------------------------------------------------------------------
    #endregion

    #region Private Methods
    // -------------------------------------------------------------------------
    private void HandleMovement()
    {
        _moveDirection = _cameraObject.forward * _inputManager.verticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * 
            _inputManager.horizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0f;

        if (isSprinting)
        {
            _moveDirection *= sprintingSpeed;
        }
        else
        {
            if (_inputManager.moveAmount >= 0.5f)
            {
                // _moveDirection = _moveDirection * runningSpeed;
                _moveDirection *= runningSpeed;
            }
            else
            {
                _moveDirection = _moveDirection * walkingSpeed;
            }
        }
        
        Vector3 movementVelocity = _moveDirection;
        _playerRigidbody.velocity = movementVelocity;
    }


    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraObject.forward * _inputManager.verticalInput;
        targetDirection += _cameraObject.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, 
            targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
    
    // -------------------------------------------------------------------------
    #endregion
    
    
    #region MonoBehavior
    // -------------------------------------------------------------------------
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }


    private void Start()
    {
        
    }
    
    
    private void Update()
    {
        
    }
    
    // -------------------------------------------------------------------------
    #endregion

}
