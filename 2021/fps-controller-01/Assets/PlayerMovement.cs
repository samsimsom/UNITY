using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;

    [SerializeField] private Transform feet;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundMask;
    
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        
    }


    private void Update()
    {
        _isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1.0f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
