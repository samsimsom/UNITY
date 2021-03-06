using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    // Variables
    public Transform targetTransform;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dispFromTarget = targetTransform.position - transform.position;
        Vector3 directionToTarget = dispFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = dispFromTarget.magnitude;

        if (distanceToTarget > 1.0f)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}
