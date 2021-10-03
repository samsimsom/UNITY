using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test02 : MonoBehaviour
{
    [SerializeField] private Transform objectPlace;
    [SerializeField] private Camera gameCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            objectPlace.position = hitInfo.point;
            objectPlace.rotation = Quaternion.FromToRotation(Vector3.up, 
                hitInfo.normal);
        }
    }
}
