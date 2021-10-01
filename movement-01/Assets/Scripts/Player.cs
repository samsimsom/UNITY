using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0;
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(inputX, inputY, inputZ);
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        // transform.position += moveAmount;
        transform.Translate(moveAmount);
    }
}
