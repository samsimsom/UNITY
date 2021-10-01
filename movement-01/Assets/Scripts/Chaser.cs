using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{

    public Transform targetTransform;
    private float speed = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 displacementFromTarget = targetTransform.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;
        
        float distanceToTarget = displacementFromTarget.magnitude;
        if (distanceToTarget > 1.5f)
        {
            transform.Translate(moveAmount);
        }
        
    }
}
