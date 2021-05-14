using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6;
    private Vector3 velocity;
    private Rigidbody myRigidbody;

    private int coinCount;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 
                                    0, 
                                    Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        myRigidbody.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider trigerCollider)
    {
        if (trigerCollider.tag == "Coin")
        {
            Destroy(trigerCollider.gameObject);
            coinCount += 1;
            Debug.Log(coinCount);
        }
    }
}
