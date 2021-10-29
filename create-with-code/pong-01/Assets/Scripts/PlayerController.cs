using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private float _screenVerticleSize;

    // Start is called before the first frame update
    void Start()
    {
        float _screenVerticleSize   = Camera.main.orthographicSize * 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Vertical");
        float velocity = dir * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * velocity;
        
        Vector3 tempPos = transform.position;
        tempPos.y = Mathf.Clamp(tempPos.y, -7.5f, 7.5f);
        transform.position = tempPos;

    }
}
