using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    
    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
