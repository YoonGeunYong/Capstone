using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TileValue
{
    grass1,
    grass2,
    grass3
}

public class Board : MonoBehaviour
{
    public static Board     boardInstance;

    public Dictionary<Vector2, TileValue> tilemap;

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
        tilemap = new Dictionary<Vector2, TileValue>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // Adjust the position to make the bottom left of the board at (0, 0).
                int             adjustedX       = x * 10;
                int             adjustedY       = y * 10;


                GameObject      tileObject      = Instantiate(tilePrefab, new Vector3(adjustedX, adjustedY, 0f), tilePrefab.transform.rotation);
                Tile            tileComponent   = tileObject.GetComponent<Tile>();
                
                tileComponent.tilePosition      = new Vector2Int(x, y);
                tileComponent.board = this;
                int tileValue = tileComponent.ApplyNatureTexture(); // Apply Perlin noise texture to the tile.

                
                tilemap.Add(new Vector2(x, y), (TileValue)tileValue - 1);
            }

    }
    
    public static Vector3 GetTilePosition(Vector2Int tilePosition)
    {
        return new Vector3(tilePosition.x * 10, tilePosition.y * 10, 0f);
    }

}
