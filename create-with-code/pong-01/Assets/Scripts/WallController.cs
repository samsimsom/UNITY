using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float MaxWallSize { get; set; }

    void Start()
    {
        MaxWallSize = 1.0f;
        transform.localScale += Vector3.up * MaxWallSize;
    }
    
}
