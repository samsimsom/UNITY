using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float smoothMoveTime = 0.1f;
    [SerializeField] private float turnSpeed = 8.0f;
    
    private float angle;
    private float smoothInputMagnitude;
    private float smooothMoveVelocity;
    private Vector3 velocity;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0.0f;
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(inputX, inputY, inputZ).normalized;
        
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smooothMoveVelocity, smoothMoveTime);
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        
        // transform.eulerAngles = Vector3.up * angle;
        // Vector3 translation = transform.forward * (moveSpeed * Time.deltaTime * smoothInputMagnitude);
        // transform.Translate(translation, Space.World);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }


    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        _rigidbody.MovePosition(_rigidbody.position + velocity * Time.deltaTime);
    }
}