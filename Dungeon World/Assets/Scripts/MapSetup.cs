using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetup : MonoBehaviour
{
    public GameObject tileObject;
    public float tileSize = 0.2f;
    public float mapSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        PopulateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateGrid()
    {
        float tileNum = mapSize / tileSize;
        for(int x = 0; x < tileNum; x++)
        {
            for (int y = 0; y < tileNum; y++)
            {
                GameObject newTile = Instantiate(tileObject, new Vector3(x * tileSize, 0, -y * tileSize), Quaternion.identity, transform);
            }
        }
    }
}
