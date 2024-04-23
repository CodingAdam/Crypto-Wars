using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int[,] tileArray;
    private int[,] ownerArray;
    public GameObject tilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        // Just a default map
        int[,] map = 
        {
            {1, 1, 1, 1, 0},
            {1, 1, 1, 1, 0},
            {1, 1, 1, 1, 1},
            {0, 1, 1, 1, 1},
            {0, 1, 1, 1, 0}
        };
        int[,] owners =
        {
            {0, 0, 0, 0, -1},
            {0, 0, 0, 0, -1},
            {0, 0, 1, 1, 1},
            {-1, 1, 1, 1, 1},
            {-1, 1, 1, 1, -1}
        };
        SetArray(map);
        SetOwner(owners);
        CreateSymmetricMap(map);
        GenerateMap();
    }
    public void SetArray(int[,] array)
    {
        tileArray = array;
    }

    public void SetOwner(int[,] array)
    {
        ownerArray = array;
    }
    public int[,] GetArray()
    {
        return tileArray;
    }
    public int[,] GetOwnership()
    {
        return ownerArray;
    }
    // Create a larger 2D array and mirror the given array to create an even P1 and P2 side (optional)
    public void CreateSymmetricMap(int[,] array)
    {
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        int[,] symmetricArray = new int[width * 2, height];
        int[,] newOwnerArray = new int[width * 2, height];

        // Mirror the given array to the right so that P1 and P2 have an even amount of starting tiles
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                symmetricArray[i, j] = array[i, j];
                symmetricArray[width * 2 - i - 1, j] = array[i, j];
                newOwnerArray[i, j] = ownerArray[i, j];
                newOwnerArray[width * 2 - i - 1, j] = ownerArray[i, j];
            }
        }
        tileArray = symmetricArray;
        ownerArray = newOwnerArray;
    }
    // Generate a map of tiles based on the given array
    public void GenerateMap()
    {
        int width = tileArray.GetLength(0);
        int height = tileArray.GetLength(1);

        // The offset is used to mimic an edge shader effect on the tiles so that they are easy to differentiate
        // If this effect is not desired then xOffset and yOffset should be set to 1
        float xOffset = 1.02f;
        float yOffset = 1.02f;

        // Instantiate tiles based on the array (where 1 represents a tile and 0 represents empty space)
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(tileArray[i, j] == 1)
                {
                    Vector3 tilePosition = new Vector3(i * xOffset, 2.0f, j * yOffset);
                    tilePrefab.GetComponent<Tile>().SetPlayer(ownerArray[i, j]);
                    GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                    //tile.transform.SetParent(transform);
                }
            }
        }
    }
}