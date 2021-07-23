using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        var hInputAxisRaw = Input.GetAxisRaw("Horizontal");
        var vInputAxiesRaw = Input.GetAxisRaw("Vertical");

        var moveDirection = new Vector3(
            hInputAxisRaw,
            0f,
            vInputAxiesRaw
            ).normalized;
        var velocity = moveDirection * (speed * Time.deltaTime);
        
        transform.Translate(velocity);
    }
}
