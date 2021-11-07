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
    

    private float _moveAmount;
    private PlayerControls _playerControls;
    private AnimatorManager _animatorManager;
    
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
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
    }
    
    
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        _moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + 
                                    Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, 
            _moveAmount);
    }


    private void HandleCameraInput()
    {
        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
    }
    
}
