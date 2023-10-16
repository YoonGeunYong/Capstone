using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile2 : MonoBehaviour
{
    public Vector2Int position;
    public Unit2 unit;

    // Initialize the tile with a given position.
    public void Init(Vector2Int position)
    {
        this.position = position;
    }

    private void OnMouseDown()
    {
        // When this tile is clicked, select the unit on it (if any).
        Board2 board = GameController2.instance.gameBoard;

        if (unit != null)
            board.selectedUnit = unit;
        else
            board.selectedUnit = null;  // Deselect the current unit if this tile is empty.

        Debug.Log("Selected Unit: " + (board.selectedUnit != null ? board.selectedUnit.name : "None"));
    }
}
