using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapGenerator : MonoBehaviour
{
    // tile edecek olan prefab
    [SerializeField] private Transform tilePrefab;
    // mapsize vector2(x, y)
    [SerializeField] private Vector2 mapSize;

    // tile outline percentage
    [SerializeField] [Range(0.0f, 1.0f)] private float outlinePercent;

    public void GenerateMap()
    {
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
                float tileX = -mapSize.x / 2 + 0.5f + x;
                float tileY = -mapSize.y / 2 + 0.5f + y;
                Vector3 tilePosition = new Vector3(tileX, 0, tileY);
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
