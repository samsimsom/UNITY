using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private Vector2 speedMinMax;

    private float _speed;
    private float _visibleHeightTreshold;

    // Start is called before the first frame update
    void Start()
    {
        _speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
        _visibleHeightTreshold = -Camera.main.orthographicSize - transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        DestroyPassedBlock();
    }


    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.Self);
    }


    private void DestroyPassedBlock()
    {
        if (transform.position.y < _visibleHeightTreshold)
        {
            Destroy(gameObject);
        }
    }
}