using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManagerr;
    private CameraManager _cameraManager;
    private PlayerLocomotion _playerLocomotion;
    
    private void Awake()
    {
        _inputManagerr = GetComponent<InputManager>();
        _cameraManager = FindObjectOfType<CameraManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }


    private void Update()
    {
        _inputManagerr.HandleAllInputs();
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
