using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float movementSpeed;
    public float speedModifier;
    public float maxSpeed;

    private int _hitCounter;
    private Rigidbody2D _rigidbody2D;


    public void MoveBall(Vector2 dir)
    {
        dir = dir.normalized;
        float speed = movementSpeed + _hitCounter * speedModifier;
        _rigidbody2D.velocity = dir * speed;
    }


    private void PositionBall(bool player)
    {
        _rigidbody2D.velocity = Vector2.zero;

        if (player)
        {
            transform.position = Vector3.left;
        }
        else
        {
            transform.position = Vector3.right;
        }
    }
    
    
    public void IncreaseSpeedModifier() 
    {
        if (_hitCounter + speedModifier <= maxSpeed)
        {
            _hitCounter++;
        }
    }

    public IEnumerator StartBallMove(bool isStartingPlayer1 = true)
    {
        PositionBall(isStartingPlayer1);
        
        _hitCounter = 0;
        yield return new WaitForSeconds(2);

        if (isStartingPlayer1)
        {
            MoveBall(new Vector2(-1.0f, 0.0f));
        }
        else
        {
            MoveBall(new Vector2(1.0f, 0.0f));
        }
    }


    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(StartBallMove());
    }

}
