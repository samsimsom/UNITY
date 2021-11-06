using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [FormerlySerializedAs("_waves")] [SerializeField] private Wave[] waves;
    [FormerlySerializedAs("_enemy")] [SerializeField] private Enemy enemy;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private int _enemiesRemainingToSpawn;
    private int _enemiesRemainingAlive;
    private float _nextSpawnTime;
    private MapGenerator _map;
    private LivingEntity _playerEntity;
    private Transform _playerTransform;
    
    private float _timeBetweenCampingChecks = 2f;
    private float _campThresholdDistance = 1.5f;
    private float _nextCampCheckTime;
    private Vector3 _campPositionOld;
    private bool _isCamping;

    private bool _isDisabled;

    private void NextWave()
    {
        _currentWaveNumber++;
        if (_currentWaveNumber - 1 < waves.Length)
        {
            _currentWave = waves[_currentWaveNumber - 1];
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

            StartCoroutine(SpawnEnemy());
        }
    }


    IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;

        Transform spawnTile = _map.GetRandomOpenTile();
        if (_isCamping)
        {
            spawnTile = _map.GetTileFromPosition(_playerTransform.position);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0f;

        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColor, flashColor, 
                Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));
            
            spawnTimer += Time.deltaTime;
            yield return null;
        }
        
        Enemy spawnedEnemy = Instantiate(enemy, 
            spawnTile.position + Vector3.up, Quaternion.identity)
            as Enemy;
        spawnedEnemy.OnDeath += OnEnemyDeath;

    }

    private void OnPlayerDeath()
    {
        _isDisabled = true;
    }
    
    
    private void OnEnemyDeath()
    {
        _enemiesRemainingAlive--;
        if (_enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }
    

    private void Start()
    {
        _playerEntity = FindObjectOfType<Player>();
        _playerTransform = _playerEntity.transform;

        _nextCampCheckTime = _timeBetweenCampingChecks + Time.time;
        _campPositionOld = _playerTransform.position;

        _playerEntity.OnDeath += OnPlayerDeath;
        
        _map = FindObjectOfType<MapGenerator>();
        NextWave();
    }


    private void Update()
    {
        if (!_isDisabled)
        {
            if (Time.time > _nextCampCheckTime)
            {
                _nextCampCheckTime = Time.time + _timeBetweenCampingChecks;
                _isCamping = (Vector3.Distance(_playerTransform.position, 
                    _campPositionOld) < _campThresholdDistance);
                _campPositionOld = _playerTransform.position;
            }
        
            Spaw();
        }

    }
}


[Serializable]
public class Wave
{
    public int enemyCount;
    public float timeBetweenSpawns;
}