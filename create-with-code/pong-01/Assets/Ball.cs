using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float speedModifier;
    [SerializeField] private float maxSpeed;

    private int _hitCounter;
    private Rigidbody2D _rigidbody2D;


    public IEnumerator StartBall(bool isStartingPlayer1 = true)
    {
        _hitCounter = 0;
        yield return new WaitForSeconds(2);

        if (isStartingPlayer1)
        {
            Move(new Vector2(-1, 0));
        }
        else
        {
            Move(new Vector2(1, 0));
        }
    }
    

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;
        float speed = movementSpeed + (_hitCounter * speedModifier);
        _rigidbody2D.velocity = direction * speed;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        StartCoroutine(StartBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
