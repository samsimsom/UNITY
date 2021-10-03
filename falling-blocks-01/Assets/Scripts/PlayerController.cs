using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float screenHalfWithInWorldUnit;
    private float playerHalfWith;

    // Start is called before the first frame update
    void Start()
    {
        playerHalfWith = transform.localScale.x / 2.0f;
        screenHalfWithInWorldUnit = (Camera.main.aspect * Camera.main.orthographicSize) + playerHalfWith;
    }

    // Update is called once per frame
    void Update()
    {
        // direction
        float inputX = Input.GetAxisRaw("Horizontal");
        // velocity = direction * speed
        float velocity = inputX * speed;
        // Vector2.right = Vector2(1, 0) x, y
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        ScreenWrapAround();
    }

    private void ScreenWrapAround()
    {
        if (transform.position.x < -screenHalfWithInWorldUnit)
        {
            transform.position = new Vector2(screenHalfWithInWorldUnit, transform.position.y);
        }

        if (transform.position.x > screenHalfWithInWorldUnit)
        {
            transform.position = new Vector2(-screenHalfWithInWorldUnit, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "block")
        {
            Destroy(gameObject);
        }
    }
}