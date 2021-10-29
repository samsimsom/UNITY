using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOne : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform ball;

    private void Start()
    {
        StartCoroutine(EnemyMovement(ball));
    }

    IEnumerator EnemyMovement(Transform target)
    {
        float distance = Vector3.Distance(transform.position,
            target.position);
        
        while ( distance < 17.0f)
        {
            transform.position = Vector3.Lerp(transform.position, 
                target.position, movementSpeed * Time.deltaTime);

            // Clampgin New Position
            Vector3 tempPos = transform.position;
            tempPos.x = 15.0f;
            tempPos.y = Mathf.Clamp(tempPos.y, -7.5f, 7.5f);
            transform.position = tempPos;
            
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
    }
    
}
