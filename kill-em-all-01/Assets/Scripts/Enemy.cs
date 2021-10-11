using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : LivingEntity
{

    public enum State
    {
        Idle,
        Chasing,
        Attacking
    };
    private State currentState;

    private Material skinMaterial;
    private Color originalColor;
    
    private NavMeshAgent pathFinder;
    private Transform target;
    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttacks = 1.0f;
    private float nextAttackTime;

    private float myCollisionRadius;
    private float targetCollisionRadius;
    
    protected override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();

        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }


    private void Update()
    {
        float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
        if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + 
                                       myCollisionRadius +
                                       targetCollisionRadius, 2))
        {
            nextAttackTime = Time.time + timeBetweenAttacks;
            StartCoroutine(Attack());
        }
    }


    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathFinder.enabled = false;
        
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, 
                attackPosition, interpolation);
            
            yield return null;
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }
    

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                // Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                Vector3 targetPosition = target.position - dirToTarget * 
                    (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                
                if (!isDead)
                {
                    pathFinder.SetDestination(targetPosition);

                }
            }
            yield return new WaitForSeconds(refreshRate);
            
        }
    }
}
