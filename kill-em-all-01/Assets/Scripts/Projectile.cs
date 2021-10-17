using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private float _speed = 10.0f;
    private float _damage = 1.0f;
    private float _lifeTime = 3.0f;
    private float _skinWith = 0.1f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 
            0.1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0]);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }


    void CheckCollisions(float movedistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, movedistance + _skinWith, 
            collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }


    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(_damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
    
    
    void OnHitObject(Collider collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(_damage);
        }
        GameObject.Destroy(gameObject);
    }
    
    
    void Update()
    {
        float moveDistance = _speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
