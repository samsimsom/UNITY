using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private float _screenVerticleSize;
    private float _verticalDir;
    
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(Camera.main.orthographicSize);
        float _screenVerticleSize   = Camera.main.orthographicSize * 2.0f;
        // float horizontalSize = verticalSize * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {

        _verticalDir = Input.GetAxisRaw("Vertical");
        float speed = _verticalDir * moveSpeed * Time.deltaTime;
        
        transform.position += new Vector3(0f, speed, 0f); 
        float playerPosition = transform.position.y;
        playerPosition = Mathf.Clamp(playerPosition, -7.5f, 7.5f);


    }
}
