using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{

    public float speed;
    private Vector2 speedRange = new Vector2(1.0f, 10.0f);

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
    }
}
