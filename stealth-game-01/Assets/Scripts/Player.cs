using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action OnReachEndOfLevel;
    
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float smoothMoveTime = 0.1f;
    [SerializeField] private float turnSpeed = 8.0f;
    
    private float angle;
    private float smoothInputMagnitude;
    private float smooothMoveVelocity;
    private Vector3 _velocity;

    private Rigidbody _rigidbody;
    private bool _disabled;
    
    
    private void Disable()
    {
        _disabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            Disable();
            if (OnReachEndOfLevel != null)
            {
                OnReachEndOfLevel(); // invoke event!
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpottedPlayer += Disable;
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = Vector3.zero;
        if (!_disabled)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = 0.0f;
            float inputZ = Input.GetAxisRaw("Vertical");
            inputDirection = new Vector3(inputX, inputY, inputZ).normalized;
        }
        
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, 
            inputMagnitude, ref smooothMoveVelocity, smoothMoveTime);
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, 
            Time.deltaTime * turnSpeed * inputMagnitude);

        _velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }


    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
    }


    private void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= Disable;
    }
}