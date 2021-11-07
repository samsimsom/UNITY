using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] Transform ground;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in ground)
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            child.GetComponent<Renderer>().material.color = new Color(r, g, b, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
