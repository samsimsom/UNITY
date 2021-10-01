using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject chairPrefab;
    private int chairCount;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10f, 10f), 0, 
                Random.Range(-10f, 10f));
            Vector3 randomSpawnRotation = Vector3.up * Random.Range(0, 360);
        
            GameObject newChair = (GameObject)Instantiate(chairPrefab, 
                randomSpawnPosition, 
                Quaternion.Euler(randomSpawnRotation));
            
            // parent
            newChair.transform.parent = transform;
            chairCount++;
            
            ClearLog();;
            Debug.Log(chairCount);
        }
        
    }
    
    public void ClearLog() // Clear Log Console!
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}