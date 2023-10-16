using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Tile[,]     tiles;

    public GameObject   tilePrefab;  // Assign this in the inspector.
    public Base         playerBase;
    public Base         enemyBase;


    public void InitBoard(int width, int height)
    {
        tiles = new Tile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // Adjust the position to make the bottom left of the board at (0, 0).
                float       adjustedX        =     x * 10;
                float       adjustedY        =     y * 10;

                GameObject  tileObject       =     Instantiate(tilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
                Tile        tileComponent    =     tileObject.GetComponent<Tile>();


                tiles[x / width, y / height] =     tileComponent;


                // Store the original grid position.
                tileComponent.gridPosition   =     new Vector2Int(x / width, y / height);
                tileComponent.board          =     this;
            }
    } 
}
