using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : LivingEntity
{
    private NavMeshAgent pathFinder;
    private Transform target;

    
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }


    IEnumerator UpdatePath()
    {
        float refreshRate = 2f;

        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 
                0, target.position.z);
            if (!isDead)
            {
                pathFinder.SetDestination(targetPosition);

            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
