using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private CameraManager _cameraManager;
    private PlayerLocomotion _playerLocomotion;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _cameraManager = FindObjectOfType<CameraManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }


    private void Update()
    {
        _inputManager.HandleAllInputs();
        _cameraManager.HandleCameraCollisionUpdate();
    }


    private void FixedUpdate()
    {
        _playerLocomotion.HandleAllMovement();
    }

    
    private void LateUpdate()
    {
        _cameraManager.HandleAllCameraMovement();
    }
}
