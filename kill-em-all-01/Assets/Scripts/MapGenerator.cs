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

    public int seed = 10;

    private List<Coord> allTileCoords;
    private Queue<Coord> shuffledTileCoords;
    

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

        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));
        
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
                // float tileX = -mapSize.x / 2 + 0.5f + x;
                // float tileY = -mapSize.y / 2 + 0.5f + y;
                Vector3 tilePosition = CoordToPosition(x, y);
                Quaternion tileRotation = Quaternion.Euler(Vector3.right * 90);
                Transform newTile = Instantiate(tilePrefab, 
                    tilePosition, 
                    tileRotation) as Transform;
                newTile.localScale = Vector3.one * (1-outlinePercent);
                // yeni yaratilan her bir tile Map - Ganerated Map altina
                // parentleniyor.
                newTile.parent = mapHolder;
            }
        }

        int obstacleCount = 10;
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.X, randomCoord.Y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * 0.5f, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
        }
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
    
    
    public struct Coord
    {
        public int X;
        public int Y;

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
