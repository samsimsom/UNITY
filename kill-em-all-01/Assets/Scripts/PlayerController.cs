using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidBody;
    private Vector3 _velocity;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 newPosition = _playerRigidBody.position + 
                              (_velocity * Time.fixedDeltaTime);
        // _playerRigidBody.MovePosition(newPosition);
        _playerRigidBody.AddForce(_velocity);

    }
    
    public void Move(Vector3 calculatedVelocity)
    {
        _velocity = calculatedVelocity;
    }


    public void LookAt(Vector3 point)
    {
        transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
    }
    
    

}
