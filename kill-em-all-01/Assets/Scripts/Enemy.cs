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

    private LivingEntity targetEntity;
    private bool hasTarget;

    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttacks = 1.0f;
    private float damage = 1;
    private float nextAttackTime;

    private float myCollisionRadius;
    private float targetCollisionRadius;

    protected override void Start()
    {
        base.Start();
        // Enemy Navigation Mesh Componet
        pathFinder = GetComponent<NavMeshAgent>();

        // Enemy Renderer.material Component
        skinMaterial = GetComponent<Renderer>().material;
        
        // Enemy Orjinal color container
        originalColor = skinMaterial.color;

        // Check Player GameObject in game! TODO: Hi!
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Enemy has a target (in this case target is Player)
            hasTarget = true;

            // Enemy Current State set to Chasing
            currentState = State.Chasing;
            // Find Player and assign target variable
            target = GameObject.FindGameObjectWithTag("Player").transform;

            // Enemy to Player Vision
            // Her bir Enemy Yaratildiginda target olarak Playeri goruyor.
            targetEntity = target.GetComponent<LivingEntity>();
            // OnDeath Event subscription
            targetEntity.OnDeath += OnTargetDeath;

            // Enemy Collision Radius
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            // Player Collision Radius
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            // Enemt to Player Movement and Calulate Path Coroutine
            StartCoroutine(UpdatePath());
        }
    }


    private void Update()
    {
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDstToTarget = (target.position - 
                                        transform.position).sqrMagnitude;

                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold +
                                               myCollisionRadius +
                                               targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }


    // Player Oldugu sirada tetiklenitor.
    private void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
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
        bool hasApplieDamage = false;

        while (percent <= 1)
        {
            if (percent >= 0.5f && !hasApplieDamage)
            {
                hasApplieDamage = true;
                targetEntity.TakeDamage(damage);
            }
            
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

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                // Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                Vector3 targetPosition = target.position - dirToTarget *
                    (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);

                if (!IsDead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}