using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _pathFinder;
    private Transform _target;

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (_target != null)
        {
            Vector3 targetPosition = new Vector3(_target.position.x, 
                0, _target.position.z);
            _pathFinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }
    
    
    void Start()
    {
        _pathFinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {
        _pathFinder.SetDestination(_target.position);
    }
}
