using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _playerSprite;
    private Vector3 _moveDelta;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var z = 0;
        
        // Reset _moveDelta
        _moveDelta = new Vector3(x, y, z);
        
        // Swap sprite direction, wether you're going left or right
        if (_moveDelta.x > 0)
        {
            _playerSprite.flipX = false;
        }
        else if (_moveDelta.x < 0)
        {
            _playerSprite.flipX = true;
        }
        
        transform.Translate(_moveDelta * Time.deltaTime);
    }
}
