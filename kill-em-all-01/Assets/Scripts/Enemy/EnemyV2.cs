using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyV2 : MonoBehaviour
{
    private NavMeshAgent _pathFinder;
    private Transform _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = _target.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        
        transform.LookAt(_target, Vector3.up);

        Vector3 movement = transform.forward * Time.deltaTime * 1.0f;
        _pathFinder.Move(movement);
    }
}
