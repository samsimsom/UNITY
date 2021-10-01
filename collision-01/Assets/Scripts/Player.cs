using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int coinCount;
    private Rigidbody myRigidBody;
    public float speed = 6;
    private Vector3 velocity;

    // Start is called before the first frame update
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0.0f;
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(inputX, inputY, inputZ);
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    private Vector3 CalculateVelocity()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0.0f;
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(inputX, inputY, inputZ);
        Vector3 direction = input.normalized;
        velocity = direction * speed;

        return velocity;
    }
    
    private void FixedUpdate()
    {
        myRigidBody.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Coin")
        {
            Destroy(triggerCollider.gameObject);
            coinCount++;
            
            print(coinCount);
        }
    }
}

