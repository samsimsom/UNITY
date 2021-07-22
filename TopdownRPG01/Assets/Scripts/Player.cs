using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private Vector3 _moveDelta;
    private SpriteRenderer _playerSprite;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        // Reset _moveDelta
        _moveDelta = new Vector3(x, y, 0);
        
        // Swap sprite direction, wether you're going left or right
        if (_moveDelta.x > 0)
        {
            _playerSprite.flipX = false;
        }
        else if (_moveDelta.x < 0)
        {
            _playerSprite.flipX = true;
        }
    }
}
