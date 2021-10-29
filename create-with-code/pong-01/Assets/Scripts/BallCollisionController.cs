using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private ScoreController scoreController;

    private void BounceFromRacket(Collision2D col)
    {
        Vector3 ballPosition = transform.position;
        Vector3 racketPosition = col.gameObject.transform.position;

        float racketHight = col.collider.bounds.size.y;

        float x;
        if (col.gameObject.name == "Player")
        {
            x = 1.0f;
        }
        else
        {
            x = -1.0f;
        }

        float y = (ballPosition.y - racketPosition.y) / racketHight;
        
        ball.IncreaseSpeedModifier();
        ball.MoveBall(new Vector2(x, y));
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "Enemy")
        {
            BounceFromRacket(other);
        }
        else if (other.gameObject.name == "BounceWallLeft")
        {
            scoreController.GoalEnemy();
            StartCoroutine(ball.StartBallMove(true));
        }
        else if (other.gameObject.name == "BounceWallRight")
        {
            scoreController.GoalPlayer();
            StartCoroutine(ball.StartBallMove(false));
        }
    }
    
}
