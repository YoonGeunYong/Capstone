using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board2 : MonoBehaviour
{
    public int width;
    public int height;
    public Tile2[,] tiles;
    public GameObject tilePrefab;

    // Currently selected unit.
    public Unit2 selectedUnit;

    // Initialize the game board with a given size.
    public Vector2Int Init(int width, int height)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile2[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                Tile2 tileComponent = tileObject.GetComponent<Tile2>();

                tileComponent.Init(new Vector2Int(x, y));

                tiles[x, y] = tileComponent;
            }

        // Assume player base is at center of the board
        return new Vector2Int(width / 2, height / 2);
    }

    // Get the Tile object at a specific position on the game board.
    public Tile2 GetTileAt(Vector2Int position)
    {
        if (position.x < 0 || position.x >= width || position.y < 0 || position.y >= height)
            return null;

        return tiles[position.x, position.y];
    }

    // Move the currently selected unit to a specific position on the game board.
    public void MoveSelectedUnitTo(Vector2Int newPosition)
    {
        if (selectedUnit != null && GetTileAt(newPosition) != null)
            selectedUnit.MoveTo(newPosition);
    }
}
