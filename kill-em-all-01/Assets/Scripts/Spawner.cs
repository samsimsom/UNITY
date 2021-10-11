using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Enemy _enemy;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private int _enemiesRemainingToSpawn;
    private int _enemiesRemainingAlive;
    private float _nextSpawnTime;
    

    private void NextWave()
    {
        _currentWaveNumber++;
        Debug.Log($"Wave: {_waves}");
        if (_currentWaveNumber - 1 < _waves.Length)
        {
            _currentWave = _waves[_currentWaveNumber - 1];
            _enemiesRemainingToSpawn = _currentWave.enemyCount;
            _enemiesRemainingAlive = _enemiesRemainingToSpawn;
        }

    }


    private void Spaw()
    {
        if (_enemiesRemainingToSpawn > 0 && Time.time > _nextSpawnTime)
        {
            _enemiesRemainingToSpawn--;
            _nextSpawnTime = Time.time + _currentWave.timeBetweenSpawns;
            
            Enemy spawnedEnemy = Instantiate(_enemy, 
                Vector3.zero, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }


    private void OnEnemyDeath()
    {
        _enemiesRemainingAlive--;
        if (_enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }
    

    void Start()
    {
        NextWave();
    }


    void Update()
    {
        Spaw();
    }
}


[Serializable]
public class Wave
{
    public int enemyCount;
    public float timeBetweenSpawns;
}