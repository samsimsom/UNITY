using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public Transform sphereTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        // Parenting
        sphereTransform.parent = transform;
        
        // Scale
        // sphereTransform.localScale = Vector3.one * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = Time.deltaTime;
        
        // Alternate Rotation
        // transform.eulerAngles += new Vector3(0, 1, 0) * 180 * Time.deltaTime;
        
        // Rotate method Object Space (Default)
        // transform.Rotate(Vector3.up * Time.deltaTime * 180);
        
        // Rotate method World Space
        transform.Rotate(Vector3.up * delta * 180, Space.World);
        
        // Transform
        transform.Translate(Vector3.forward * delta * 5, Space.Self);

        // Scale
        sphereTransform.localScale += Vector3.one * 0.1f * delta;
    }
}
