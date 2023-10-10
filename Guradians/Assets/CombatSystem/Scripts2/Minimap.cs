using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject minimapTilePrefab;

    private MinimapTile[,] minimapTiles;

    public void Init(Board2 gameBoard)
    {
        int width = gameBoard.width;
        int height = gameBoard.height;

        minimapTiles = new MinimapTile[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                GameObject tileObject = Instantiate(minimapTilePrefab, transform);
                MinimapTile tileComponent = tileObject.GetComponent<MinimapTile>();

                tileComponent.Init(new Vector2Int(x, y));

                // Adjust position to fit within the minimap UI...

                minimapTiles[x, y] = tileComponent;
            }
    }

    // Other methods...
}

