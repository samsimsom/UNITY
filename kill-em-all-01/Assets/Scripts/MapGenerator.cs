using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = System.Random;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Map[] maps;
    [SerializeField] private int mapIndex;
    
    // tile edecek olan prefab
    [SerializeField] private Transform tilePrefab;

    // obstacle prefab
    [SerializeField] private Transform obstaclePrefab;

    // tile outline percentage
    [SerializeField] [Range(0.0f, 1.0f)] private float outlinePercent;
    
    [SerializeField] [Range(0.1f, 10.0f)] private float tileSize;
    
    // Navigation Mesh Ground
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private Transform navMesh;

    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;

    private Map currentMap;


    public void GenerateMap()
    {
        currentMap = maps[mapIndex];
        Random rnd = new Random(currentMap.seed);
        
        allTileCoords = new List<Coord>();
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord>(
            Utility.ShuffleArray(allTileCoords.ToArray(), currentMap.seed));
        
        // tile map center
        // mapCenter = new Coord((int)(currentMap.mapSize.x / 2), (int)(currentMap.mapSize.y / 2));

        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        // holderName degiskenin sakladigi isim ile bir adet.
        // gameObjesi yaratiliyor - mapHolder.
        // mapHolder scriptin bagli oldugu Map objesine parant oluyor.
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // tile loop
        for (int x = 0; x < currentMap.mapSize.x; x++) // row
        {
            for (int y = 0; y < currentMap.mapSize.y; y++) // column
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Quaternion tileRotation = Quaternion.Euler(Vector3.right * 90);
                Transform newTile = Instantiate(tilePrefab, tilePosition,
                    tileRotation) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
                // yeni yaratilan her bir tile Map - Ganerated Map altina
                // parentleniyor.
                newTile.parent = mapHolder;
            }
        }

        // Spawning Obstacles
        bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

        int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
        int currentObstacleCount = 0;
        
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;
            
            if (randomCoord != currentMap.MapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                // 
                float obstacleHeight = Mathf.Lerp(
                    currentMap.minObstacleHeight, 
                    currentMap.maxObstacleHeight, 
                    (float)rnd.NextDouble());
                
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);

                // Obstacle Position
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition,
                    Quaternion.identity) as Transform;

                newObstacle.parent = mapHolder;
                
                // Obstacle Scale
                newObstacle.localScale = new Vector3(
                    (1 - outlinePercent) * tileSize,
                    obstacleHeight, 
                    (1 - outlinePercent) * tileSize);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
        
        NavMeshGenerator();

    }


    private bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[
            obstacleMap.GetLength(0), 
            obstacleMap.GetLength(1)
        ];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.MapCenter);
        mapFlags[currentMap.MapCenter.x, currentMap.MapCenter.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 
                            && neighbourX < obstacleMap.GetLength(0) 
                            && neighbourY >= 0 
                            && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && 
                                !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }


    private void NavMeshGenerator()
    {
        // navmesh size fix with map size
        navMesh.localScale = new Vector3((currentMap.mapSize.x * tileSize)/10.0f,
            1.0f, (currentMap.mapSize.y * tileSize)/10.0f);
        navMeshSurface.BuildNavMesh();
    }
    
    
    Vector3 CoordToPosition(int x, int y)
    {
        float tileX = -currentMap.mapSize.x / 2.0f + 0.5f + x;
        float tileY = -currentMap.mapSize.y / 2.0f + 0.5f + y;

        return new Vector3(tileX, 0, tileY) * tileSize;
    }

    
    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }


    #pragma warning disable CS0660, CS0661
    [Serializable]
    public struct Coord
    {
        [FormerlySerializedAs("X")] public int x;
        [FormerlySerializedAs("Y")] public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }
        
        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }
    #pragma warning restore CS0660, CS0661

    
    void Start()
    {
        GenerateMap();
    }
    
}


[Serializable]
public class Map
{
    [SerializeField] public MapGenerator.Coord mapSize;
    [SerializeField] [Range(0.0f, 1.0f)] public float obstaclePercent;
    [SerializeField] public int seed;
    [SerializeField] public float minObstacleHeight;
    [SerializeField] public float maxObstacleHeight;
    [SerializeField] public Color foregroundColor;
    [SerializeField] public Color backgrodunColor;


    public MapGenerator.Coord MapCenter
    {
        get
        {
            return new MapGenerator.Coord(mapSize.x / 2, mapSize.y / 2);
        }
    }
}