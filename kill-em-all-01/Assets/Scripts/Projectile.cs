using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private float speed = 10.0f;

    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


    void CheckCollisions(float movedistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, movedistance, 
            collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }


    void OnHitObject(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.name);
        GameObject.Destroy(gameObject);
    }
    
    
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
