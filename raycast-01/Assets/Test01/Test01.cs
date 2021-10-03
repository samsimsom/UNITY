using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.right);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100.0f, layerMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            
            // hitInfo
            Debug.Log(hitInfo.collider.gameObject.name);
            
            // hitInfo access all data to hit game objects
            hitInfo.collider.gameObject.transform.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, 
                Color.green);
        }
    }
}
