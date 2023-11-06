using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board     boardInstance;

    public Tile[,]          tiles;

    public GameObject       tilePrefab;  // Assign this in the inspector.
    public Base             playerBase;
    public Base             enemyBase;




    private void Awake()
    {

        if (boardInstance == null)
        {
            boardInstance = this;
        }

        else if (boardInstance != this)
        {
            Destroy(gameObject);
        }

    }


    public void InitBoard(int width, int height)
    {

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // Adjust the position to make the bottom left of the board at (0, 0).
                int             adjustedX       = x * 10;
                int             adjustedY       = y * 10;


                GameObject      tileObject      = Instantiate(tilePrefab, new Vector3(adjustedX, 0, adjustedY), tilePrefab.transform.rotation);
                Tile            tileComponent   = tileObject.GetComponent<Tile>();


                tileComponent.gridPosition      = new Vector2Int(adjustedX, adjustedY);
                tileComponent.board = this;
                tileComponent.ApplyNatureTexture(); // Apply Perlin noise texture to the tile.

                tiles[x, y]                     = tileComponent;
            }

    }

}
