using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [FormerlySerializedAs("_settings")] 
    [SerializeField] private MoveSettings settings = null;

    private Vector3 _moveDirection;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        DefaultMovement();
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inpuyY = Input.GetAxis("Vertical");
            Vector2 input = new Vector2(inputX, inpuyY);

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f;
            }

            _moveDirection.x = input.x * settings.speed;
            _moveDirection.z = input.y * settings.speed;
            _moveDirection.y = -settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _moveDirection.y -= settings.gravity * Time.deltaTime;
        }
    }

    private void Jump()
    {
        _moveDirection.y += settings.jumpForce;
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Debug.Log(hit.normal);
    // }
    
}