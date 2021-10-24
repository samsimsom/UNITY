using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    // tile edecek olan prefab
    [SerializeField] private Transform tilePrefab;

    // obstacle prefab
    [SerializeField] private Transform obstaclePrefab;

    // mapsize vector2(x, y)
    [SerializeField] private Vector2 mapSize;

    // tile outline percentage
    [SerializeField] [Range(0.0f, 1.0f)] private float outlinePercent;

    // obstacle generate seed
    [SerializeField] [Range(0, 1000)] private int seed;

    // obstacle percent
    [SerializeField] [Range(0.0f, 1.0f)] private float obstaclePercent;

    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;
    private Coord mapCenter;


    public void GenerateMap()
    {
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord>(
            Utility.ShuffleArray(allTileCoords.ToArray(), seed));
        
        // tile map center
        mapCenter = new Coord((int)(mapSize.x / 2), (int)(mapSize.y / 2));

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
        for (int x = 0; x < mapSize.x; x++) // row
        {
            for (int y = 0; y < mapSize.y; y++) // column
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Quaternion tileRotation = Quaternion.Euler(Vector3.right * 90);
                Transform newTile = Instantiate(tilePrefab, tilePosition,
                    tileRotation) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                // yeni yaratilan her bir tile Map - Ganerated Map altina
                // parentleniyor.
                newTile.parent = mapHolder;
            }
        }

        // Obstacles
        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
        int currentObstacleCount = 0;
        
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.X, randomCoord.Y] = true;
            currentObstacleCount++;
            
            if (randomCoord != mapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.X, randomCoord.Y);

                Transform newObstacle = Instantiate(obstaclePrefab,
                    obstaclePosition + Vector3.up * 0.5f,
                    Quaternion.identity) as Transform;

                newObstacle.parent = mapHolder;
            }
            else
            {
                obstacleMap[randomCoord.X, randomCoord.Y] = false;
                currentObstacleCount--;
            }
        }
    }


    private bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[
            obstacleMap.GetLength(0), 
            obstacleMap.GetLength(1)
        ];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCenter);
        mapFlags[mapCenter.X, mapCenter.Y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.X + x;
                    int neighbourY = tile.Y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 
                            && neighbourX < obstacleMap.GetLength(0) 
                            && neighbourY >= 0 
                            && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
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

        int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }
    

    Vector3 CoordToPosition(int x, int y)
    {
        float tileX = -mapSize.x / 2 + 0.5f + x;
        float tileY = -mapSize.y / 2 + 0.5f + y;

        return new Vector3(tileX, 0, tileY);
    }


    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }


    #pragma warning disable CS0660, CS0661
    public struct Coord
    {
        public int X;
        public int Y;

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }
        
        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
    }
    #pragma warning restore CS0660, CS0661


    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }
    
}