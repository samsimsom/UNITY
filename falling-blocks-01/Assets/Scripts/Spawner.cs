using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingBlockPrefab;
    private Vector2 screenHalfWithInWorldUnits;

    public float secondsBerweenSpawns = 1.0f;
    private float nextSpawnTime;

    public Vector2 blockSizeMinMax;
    public float spawnAngleMax;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfWithInWorldUnits =
            new Vector2(Camera.main.aspect * Camera.main.orthographicSize,
                Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + secondsBerweenSpawns;
            
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            print(spawnAngle);
            float blockSize = Random.Range(blockSizeMinMax.x, blockSizeMinMax.y);
            
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfWithInWorldUnits.x, 
                    screenHalfWithInWorldUnits.x), 
                screenHalfWithInWorldUnits.y + blockSize);

            GameObject block = (GameObject)Instantiate(fallingBlockPrefab, 
                spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            
            block.transform.localScale = new Vector3(blockSize, blockSize, 1);
            block.transform.parent = transform;
        }

    }
}