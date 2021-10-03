using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Event
    public event System.Action OnPlayerDeath;
    
    [SerializeField] private float speed;
    private float _screenHalfWithInWorldUnit;
    private float _playerHalfWith;

    // Start is called before the first frame update
    void Start()
    {
        // Playerin genisliginin yarisini hepsaliyor.
        _playerHalfWith = transform.localScale.x / 2.0f;
        // Ekranin genisliginin yarisina playerin henisliginin yarisini
        // ekliyor. ScreenWrapAround icin kullanacak.
        _screenHalfWithInWorldUnit = (Camera.main.aspect * Camera.main.orthographicSize) + _playerHalfWith;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ScreenWrapAround();
    }

    private void Movement()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // direction
        float velocity = inputX * speed; // velocity = direction * speed
        // Vector2.right = Vector2(1, 0) x, y
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
    }
    
    private void ScreenWrapAround()
    {
        if (transform.position.x < -_screenHalfWithInWorldUnit)
        {
            transform.position = new Vector2(_screenHalfWithInWorldUnit, 
                transform.position.y);
        }

        if (transform.position.x > _screenHalfWithInWorldUnit)
        {
            transform.position = new Vector2(-_screenHalfWithInWorldUnit, 
                transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "block")
        {
            // Direct connection
            // FindObjectOfType<GameOver>().OnGameOver();

            if (OnPlayerDeath != null)
            {
                OnPlayerDeath(); // invoke
            }
            
            Destroy(gameObject);
        }
    }
}