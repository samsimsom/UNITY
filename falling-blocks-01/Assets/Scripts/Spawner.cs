using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject fallingBlockPrefab;

    private Vector2 screenHalfWithInWorldUnits;

    [SerializeField] private Vector2 secondsBerweenSpawnsMinMax;

    private float nextSpawnTime;

    [SerializeField] private Vector2 blockSizeMinMax;
    [SerializeField] private float spawnAngleMax;

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
            // Difficulty
            float secondsBetweenSpawns = Mathf.Lerp(secondsBerweenSpawnsMinMax.y, 
                secondsBerweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());

            nextSpawnTime = Time.time + secondsBetweenSpawns;

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float blockSize = Random.Range(blockSizeMinMax.x, blockSizeMinMax.y);

            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfWithInWorldUnits.x,
                    screenHalfWithInWorldUnits.x), screenHalfWithInWorldUnits.y + blockSize);

            GameObject block = (GameObject)Instantiate(fallingBlockPrefab,
                spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));

            block.transform.localScale = new Vector3(blockSize, blockSize, 1);
            block.transform.parent = transform;
        }
    }
}