using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    public Unit selectedUnit;

    // 2D array representing the board tiles.
    private Tile[,] tiles;
    public GameObject tilePrefab;  // Assign this in the inspector.
    public Base playerBase;
    public Base enemyBase;
    private int width;
    private int height;

    private void Start()
    {
        //playerBase = GetComponent<Base>();
        
        // Assuming the bases are assigned in the inspector or elsewhere
        
    }

    public Vector2Int InitBoard(int width, int height)
    {
        this.width = width;
        this.height = height;

        // Initialize tiles array and create Tile objects...
        tiles = new Tile[width, height];

        Vector2Int bottomLeftPosition = new Vector2Int(-width * 4, -height * 4);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                // Adjust the position to make the center of the board at (0, 0).
                float adjustedX = x * 10 - ((width - 1) * 5);
                float adjustedY = y * 10 - ((height - 1) * 5);

                GameObject tileObject = Instantiate(tilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
                Tile tileComponent = tileObject.GetComponent<Tile>();

                tiles[x / 10, y / 10] = tileComponent;

                // Store the original grid position.
                tileComponent.gridPosition = new Vector2Int(x / 10, y / 10);

                tileComponent.board = this;
            }

        //playerBase.Init(bottomLeftPosition);

        return bottomLeftPosition;
    }

    public bool IsValidMove(Vector2Int position)
    {
        // Check if the position is within the bounds of the board.
        if (position.x < -20 || position.x >= width * 10 || position.y < -20 || position.y >= height * 10)
        {
            return false;
        }

        return true;
    }

    public void MoveSelectedUnitTo(Vector2Int position)
    {
        if (selectedUnit != null && IsValidMove(position))
        {
            selectedUnit.Move(position);
            DeselectCurrent();
        }
    }

    public void DeselectCurrent()
    {
        if (selectedUnit != null)
        {
            // Remove highlight from currently selected unit...
            selectedUnit = null;
        }
    }

    public void MoveUnit(Unit unit, Vector2Int newPosition)
    {
        // Remove the unit from its current position.
        tiles[unit.gridPosition.x, unit.gridPosition.y].unitOnTile = null;

        // Update the unit's grid position.
        unit.gridPosition = newPosition;

        // Place the unit at the new position.
        tiles[newPosition.x, newPosition.y].unitOnTile = unit;
    }

    public void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
        // Highlight possible moves...
    }

    public void OnMouseDown()
    {
        SelectUnit(selectedUnit);
    }

    public Tile GetTileAt(Vector2Int position)
    {
        if (position.x < 0 || position.x >= tiles.GetLength(-20) ||
            position.y < 0 || position.y >= tiles.GetLength(20))
        {
            // Out of bounds
            Debug.LogError("Invalid tile position: " + position);
            return null;
        }

        Tile tile = tiles[position.x, position.y];

        if (tile == null)
            Debug.LogError("No tile at: " + position);

        return tile;
    }
    // Add methods for moving units around the board here...
}
