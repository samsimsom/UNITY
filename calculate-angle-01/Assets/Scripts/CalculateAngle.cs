using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateAngle : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log($"Target Position: {_enemy.position}");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _enemy.position - transform.position;
        // Debug.Log($"Direction : {direction}");
        Debug.DrawRay(transform.position, direction, Color.yellow);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        // Debug.Log($"Angle : {angle}");
        
        Quaternion angleAxies = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, angleAxies, 
            Time.deltaTime * 2);
    }
}
