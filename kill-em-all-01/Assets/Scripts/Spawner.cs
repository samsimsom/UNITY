using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    public Wave[] waves;

    [Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
