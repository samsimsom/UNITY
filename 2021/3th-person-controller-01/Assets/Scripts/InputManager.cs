using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class InputManager : MonoBehaviour
{
    #region Class Variables
    // -------------------------------------------------------------------------
    [ShowOnly] public Vector2 movementInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;
    
    [ShowOnly] public Vector2 cameraInput;
    [HideInInspector] public float cameraInputX;
    [HideInInspector] public float cameraInputY;
    

    [HideInInspector] public float moveAmount;
    private PlayerLocomotion _playerLocomotion;
    private PlayerControls _playerControls;
    private AnimatorManager _animatorManager;

    [ShowOnly] public bool sprintInput;
    
    // -------------------------------------------------------------------------
    #endregion
    
    
    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            
            // Movement input girdilerini okuyor ve movementInputa aktariyor
            _playerControls.PlayerMovements.Movement.performed += 
                i => movementInput = i.ReadValue<Vector2>();

            // Camera input girdilerini okuyor ve cameraInputa aktariyor
            _playerControls.PlayerMovements.Camera.performed +=
                i => cameraInput = i.ReadValue<Vector2>();
            
            // Player Actions Inputs
            _playerControls.PlayerActions.Sprinting.performed += 
                i => sprintInput = true;
            _playerControls.PlayerActions.Sprinting.canceled +=
                i => sprintInput = false;
        }
        
        _playerControls.Enable();
    }

    
    private void OnDisable()
    {
        _playerControls.Disable();
    }


    private void Awake()
    {
        _animatorManager = GetComponent<AnimatorManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleCameraInput();
    }
    
    
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + 
                                    Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, 
            moveAmount, _playerLocomotion.isSprinting);
    }


    private void HandleSprintingInput()
    {
        if (sprintInput && moveAmount > 0.5f)
        {
            _playerLocomotion.isSprinting = true;
        }
        else
        {
            _playerLocomotion.isSprinting = false;
        }
    }
    
    
    private void HandleCameraInput()
    {
        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
    }
    
}
