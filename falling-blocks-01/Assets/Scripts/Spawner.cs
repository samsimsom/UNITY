using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject fallingBlockPrefab;

    private Vector2 _screenHalfWithInWorldUnits;

    [SerializeField] private Vector2 secondsBerweenSpawnsMinMax;

    private float _nextSpawnTime;

    [SerializeField] private Vector2 blockSizeMinMax;
    [SerializeField] private float spawnAngleMax;

    // Start is called before the first frame update
    void Start()
    {
        _screenHalfWithInWorldUnits =
            new Vector2(Camera.main.aspect * Camera.main.orthographicSize,
                Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawnTime)
        {
            // Difficulty
            float secondsBetweenSpawns = Mathf.Lerp(secondsBerweenSpawnsMinMax.y, 
                secondsBerweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());

            _nextSpawnTime = Time.time + secondsBetweenSpawns;

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float blockSize = Random.Range(blockSizeMinMax.x, blockSizeMinMax.y);

            Vector2 spawnPosition = new Vector2(Random.Range(-_screenHalfWithInWorldUnits.x,
                    _screenHalfWithInWorldUnits.x), _screenHalfWithInWorldUnits.y + blockSize);

            GameObject block = (GameObject)Instantiate(fallingBlockPrefab,
                spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));

            block.transform.localScale = new Vector3(blockSize, blockSize, 1);
            block.transform.parent = transform;
        }
    }
}